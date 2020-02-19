using D2Solo.UI;
using NetFwTypeLib;
using System;

namespace D2Solo.Backend
{
    class FirewallCommands
    {
        //firewall rule names
        private static readonly string OUT_TCP_NAME = "D2Solo-OutBoundTCP";
        private static readonly string IN_TCP_NAME = "D2Solo-InBoundTCP";
        private static readonly string OUT_UDP_NAME = "D2Solo-OutBoundUDP";
        private static readonly string IN_UDP_NAME = "D2Solo-InBoundUDP";

        public static void BlockMatchmakingPorts()
        {
            //going to try out binding to socket manually without firewalls and seeing if that works





            //creating new rules
            INetFwRule2 OutBoundTCP = (INetFwRule2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FWRule"));
            INetFwRule2 OutBoundUDP = (INetFwRule2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FWRule"));
            INetFwRule2 InBoundTCP = (INetFwRule2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FWRule"));
            INetFwRule2 InBoundUDP = (INetFwRule2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FWRule"));

            //names
            OutBoundTCP.Name = OUT_TCP_NAME;
            OutBoundUDP.Name = OUT_UDP_NAME;
            InBoundTCP.Name = IN_TCP_NAME;
            InBoundUDP.Name = IN_UDP_NAME;

            //enabling
            OutBoundTCP.Enabled = true;
            OutBoundUDP.Enabled = true;
            InBoundTCP.Enabled = true;
            InBoundUDP.Enabled = true;

            //specifying block as action
            OutBoundTCP.Action = NET_FW_ACTION_.NET_FW_ACTION_BLOCK;
            OutBoundUDP.Action = NET_FW_ACTION_.NET_FW_ACTION_BLOCK;
            InBoundTCP.Action = NET_FW_ACTION_.NET_FW_ACTION_BLOCK;
            InBoundUDP.Action = NET_FW_ACTION_.NET_FW_ACTION_BLOCK;

            //setting TCP or UDP
            OutBoundTCP.Protocol = 6;
            OutBoundUDP.Protocol = 17;
            InBoundTCP.Protocol = 6;
            InBoundUDP.Protocol = 17;

            //specifying ports to block
            OutBoundTCP.LocalPorts = "1935,3097,3478-3480";
            OutBoundUDP.LocalPorts = "1935,3097,3478-3480";
            InBoundTCP.LocalPorts = "1935,3097,3478-3480";
            InBoundUDP.LocalPorts = "1935,3097,3478-3480";

            //direction
            OutBoundTCP.Direction = NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_OUT;
            OutBoundUDP.Direction = NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_OUT;
            InBoundTCP.Direction = NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_IN;
            InBoundUDP.Direction = NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_IN;

            //fetching firewall policy
            INetFwPolicy2 currentFWPolicy = (INetFwPolicy2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwPolicy2"));

            //adding new rules
            currentFWPolicy.Rules.Add(OutBoundTCP);
            currentFWPolicy.Rules.Add(OutBoundUDP);
            currentFWPolicy.Rules.Add(InBoundTCP);
            currentFWPolicy.Rules.Add(InBoundUDP);

            MainPageViewModel.getInstance().SoloEnabled = true;
        }

        public static void OpenMatchmakingPorts()
        {
            //fetching firewall policy
            INetFwPolicy2 currentFWPolicy = (INetFwPolicy2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwPolicy2"));

            //removing rules
            currentFWPolicy.Rules.Remove(OUT_TCP_NAME);
            currentFWPolicy.Rules.Remove(OUT_UDP_NAME);
            currentFWPolicy.Rules.Remove(IN_TCP_NAME);
            currentFWPolicy.Rules.Remove(IN_UDP_NAME);

            MainPageViewModel.getInstance().SoloEnabled = false;
        }


        //call this on application launch to set "SoloEnabled" property (should always start disabled but
            //if process is killed through Task Manager for example the normal shutdown stuff won't get done)
        /*public static Dictionary<int, bool> PortStatus()
        {
            //fetching firewall policy
            INetFwPolicy2 currentFWPolicy = (INetFwPolicy2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwPolicy2"));

            currentFWPolicy.IsRuleGroupCurrentlyEnabled("OutBoundTCP");

        }*/
    }
}
