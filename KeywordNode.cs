
using System;

namespace bmesg2csharp
{
    class KeywordNode : INode {

        private string keyword;

        public KeywordNode(string keyword) 
        {
            this.keyword = keyword;
        }

        public override string ToString()
        {
            return keyword;
        }

        public string GetKeyword()
        {
            return keyword;
        }
    }
}
