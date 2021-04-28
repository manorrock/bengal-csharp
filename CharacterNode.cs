
namespace bmesg2csharp
{
    class CharacterNode {

        private char character;

        public CharacterNode(char _character) 
        {
            this.character = _character;
        }

        public override string ToString()
        {
            return character.ToString();
        }
    }
}
