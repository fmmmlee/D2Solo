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
            INetFwRule2 OutBoundUDP = (INetFwRule2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FWRule"));
            INetFwRule2 OutBoundTCP = (INetFwRule2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FWRule"));
            INetFwRule2 InBoundUDP = (INetFwRule2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FWRule"));
            INetFwRule2 InBoundTCP = (INetFwRule2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FWRule"));

            //names
            OutBoundUDP.Name = OUT_UDP_NAME;
            OutBoundTCP.Name = OUT_TCP_NAME;
            InBoundUDP.Name = IN_UDP_NAME;
            InBoundTCP.Name = IN_TCP_NAME;

            //enabling
            OutBoundUDP.Enabled = true;
            OutBoundTCP.Enabled = true;
            InBoundUDP.Enabled = true;
            InBoundTCP.Enabled = true;

            //specifying block as action
            OutBoundUDP.Action = NET_FW_ACTION_.NET_FW_ACTION_BLOCK;
            OutBoundTCP.Action = NET_FW_ACTION_.NET_FW_ACTION_BLOCK;
            InBoundUDP.Action = NET_FW_ACTION_.NET_FW_ACTION_BLOCK;
            InBoundTCP.Action = NET_FW_ACTION_.NET_FW_ACTION_BLOCK;

            //setting UDP/TCP
            OutBoundUDP.Protocol = 17;
            OutBoundTCP.Protocol = 6;
            InBoundUDP.Protocol = 17;
            InBoundTCP.Protocol = 6;

            //specifying ports to block
            OutBoundUDP.RemotePorts = "27000-27100";
            OutBoundTCP.RemotePorts = "27000-27100";
            InBoundUDP.RemotePorts = "27000-27100";
            InBoundTCP.RemotePorts = "27000-27100";

            //direction
            OutBoundUDP.Direction = NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_OUT;
            OutBoundTCP.Direction = NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_OUT;
            InBoundUDP.Direction = NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_IN;
            InBoundTCP.Direction = NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_IN;

            //fetching firewall policy
            INetFwPolicy2 currentFWPolicy = (INetFwPolicy2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwPolicy2"));

            //adding new rules
            currentFWPolicy.Rules.Add(OutBoundUDP);
            currentFWPolicy.Rules.Add(OutBoundTCP);
            currentFWPolicy.Rules.Add(InBoundUDP);
            currentFWPolicy.Rules.Add(InBoundTCP);

            MainPageViewModel.getInstance().SoloEnabled = true;
        }

        public static void OpenMatchmakingPorts()
        {
            //fetching firewall policy
            INetFwPolicy2 currentFWPolicy = (INetFwPolicy2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwPolicy2"));

            //removing rules
            currentFWPolicy.Rules.Remove(OUT_UDP_NAME);
            currentFWPolicy.Rules.Remove(OUT_TCP_NAME);
            currentFWPolicy.Rules.Remove(IN_UDP_NAME);
            currentFWPolicy.Rules.Remove(IN_TCP_NAME);

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
