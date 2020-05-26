using System;
using System.Windows.Forms;
using NetFwTypeLib;
using System.IO;
using System.Security.Principal;

namespace ProcessConnectionsManager.Block
{
    public enum FirewallStatus
    {
        Enabled,
        PrivateDisabled,
        PublicDisabled,
        AllDisabled
    }

    public static class WinFirewall
    {
        private const string RuleName = "Process Connections Manager Block";
        private static INetFwPolicy2 FwPolicy2;

        public static void AddFirewallRule(NET_FW_IP_PROTOCOL_ protocol, string remoteAddress, string port, string processPath)
        {
            if (!IsUserAdmin())
            {
                MessageBox.Show("You need to run the application as administrator to use this feature.", "Admin rights required", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            INetFwPolicy2 fwPolicy2 = GetFwPolicy2();
            var currentProfiles = fwPolicy2.CurrentProfileTypes;

            // New rule.
            INetFwRule2 inboundRule = (INetFwRule2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FWRule"));
            inboundRule.Enabled = true;
            inboundRule.Action = NET_FW_ACTION_.NET_FW_ACTION_BLOCK;
            inboundRule.Direction = NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_IN;
            inboundRule.Protocol = (int)protocol;
            inboundRule.LocalPorts = port;
            inboundRule.RemoteAddresses = remoteAddress;
            inboundRule.ApplicationName = processPath;
            inboundRule.Name = RuleName;
            inboundRule.Profiles = currentProfiles;

            // Adding rule.
            fwPolicy2.Rules.Add(inboundRule);
        }

        /// <summary>
        /// Removes all windows firewall rules with the specified name.
        /// </summary>
        public static void RemoveAllRules()
        {
            INetFwPolicy2 fwPolicy2 = GetFwPolicy2();

            try
            {
                while (true)
                {
                    fwPolicy2.Rules.Item(RuleName); // Throws FileNotFoundException if rule not found.
                    fwPolicy2.Rules.Remove(RuleName);
                }
            }
            catch (FileNotFoundException) { } // Rule doesn't exist.
        }

        public static FirewallStatus IsFirewallEnabled()
        {
            INetFwPolicy2 fwPolicy2 = GetFwPolicy2();

            FirewallStatus firewallStatus = FirewallStatus.Enabled;
            var currentProfiles = fwPolicy2.CurrentProfileTypes;

            if (Convert.ToBoolean(currentProfiles & (int)NET_FW_PROFILE_TYPE2_.NET_FW_PROFILE2_PRIVATE) && !fwPolicy2.FirewallEnabled[NET_FW_PROFILE_TYPE2_.NET_FW_PROFILE2_PRIVATE])
            {
                firewallStatus = FirewallStatus.PrivateDisabled;
            }

            if (Convert.ToBoolean(currentProfiles & (int)NET_FW_PROFILE_TYPE2_.NET_FW_PROFILE2_PUBLIC) && !fwPolicy2.FirewallEnabled[NET_FW_PROFILE_TYPE2_.NET_FW_PROFILE2_PUBLIC])
            {
                if (firewallStatus == FirewallStatus.PrivateDisabled)
                {
                    firewallStatus = FirewallStatus.AllDisabled;
                }
                else
                {
                    firewallStatus = FirewallStatus.PublicDisabled;
                }
            }

            return firewallStatus;
        }

        private static INetFwPolicy2 GetFwPolicy2()
        {
            return FwPolicy2 ?? (FwPolicy2 = (INetFwPolicy2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwPolicy2")));
        }

        private static bool IsUserAdmin()
        {
            return new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);
        }
    }
}