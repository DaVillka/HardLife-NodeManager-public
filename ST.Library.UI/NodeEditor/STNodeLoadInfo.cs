using System.Collections.Generic;

namespace ST.Library.UI.NodeEditor
{
    internal class STNodeLoadInfo
    {
        private List<STNode> Nodes { get; }
        private Dictionary<long, STNodeOption> Dic { get; }
        private HashSet<STNodeOption> Hs { get; }
        private List<(long, long)> ConnectInfo { get; }

        public STNodeLoadInfo(List<STNode> nodes, Dictionary<long, STNodeOption> dic, HashSet<STNodeOption> hs, List<(long, long)> connectInfo)
        {
            Nodes = nodes;
            Dic = dic;
            Hs = hs;
            ConnectInfo = connectInfo;
        }
    }
}
