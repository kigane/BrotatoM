using QFramework;

namespace BrotatoM
{
    public class CharacterSpritePathQuery : AbstractQuery<string>
    {
        public int id;

        public CharacterSpritePathQuery(int charaId)
        {
            id = charaId;
        }

        protected override string OnDo()
        {
            return this.GetModel<CharacterConfigModel>().GetConfigItemById(id).Path;
        }
    }
}
