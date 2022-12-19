using System.Text;

namespace StringFormatter
{
    public class StringFormatter : IStringFormatter
    { 
        private enum State
        {
            Default, Character, Open, Close 
        }

        private string _digits = "0123456789";
        private string _identifierChars = "abcdefghijklmnopqrstuvwxyz_ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        private State state = State.Default;

        public static readonly StringFormatter Shared = new StringFormatter();
        public string Format(string template, object target)
        {
            StringBuilder current = new StringBuilder();

            for(int i = 0; i < template.Length; i++)
            {
                switch(state)
                {
                    case State.Default:
                        if(template[i] == '{')
                        {
                            state = State.Open;
                        } else if(template[i] == '}')
                        {
                            state = State.Close;
                        }
                        else
                        {
                            state = State.Default;
                        }
                        break;
                    case State.Open:
                        if(_identifierChars.Contains(template[i]))//identifier char
                        {
                            current.Append(template[i]);
                            state = State.Character;
                        }
                        else if(template[i] == '{')
                        {
                            state = State.Default;
                        }
                        else throw new Exception();
                        break;
                    case State.Character:
                        if(_identifierChars.Contains(template[i]) ||
                            _digits.Contains(template[i]))
                        {
                            current.Append(template[i]);
                            state = State.Character;
                        }
                        else if(template[i] == '}')
                        {
                            state = State.Default;
                        }
                        else throw new Exception();
                        break;
                    case State.Close:
                        if(template[i] == '}')
                        {
                            state = State.Default;
                        }
                        else throw new Exception();
                        break;
                }
            }





            return "";
        }
    }
}