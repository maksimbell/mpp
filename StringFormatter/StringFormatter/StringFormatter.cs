using StringFormatter;
using System.Text;

namespace StringFormatting
{
    public class StringFormatter : IStringFormatter
    {
        public static readonly StringFormatter Shared = new StringFormatter();
        private enum State
        {
            Default, Character, Open, Close 
        }

        private FormattingCache _cache = new FormattingCache();

        private string _digits = "0123456789";
        private string _identifierChars = "abcdefghijklmnopqrstuvwxyz_ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        private State state = State.Default;
        public string Format(string template, object target)
        {
            StringBuilder current = new StringBuilder();
            StringBuilder result = new StringBuilder();

            for(int i = 0; i < template.Length; i++)
            {
                switch(state)
                {
                    case State.Default:
                        if(template[i] == '{')
                        {
                            result.Append(template[i]);
                            state = State.Open;
                        } else if(template[i] == '}')
                        {
                            result.Append(template[i]);
                            state = State.Close;
                        }
                        else
                        {
                            result.Append(template[i]);
                            state = State.Default;
                        }
                        break;
                    case State.Open:
                        if(_identifierChars.Contains(template[i]))//identifier char
                        {
                            result.Remove(result.Length - 1, 1);
                            current.Clear();
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
                            string identifier = _cache.GetOrAdd(current.ToString(), target);
                            if(identifier != null)
                            {
                                result.Append(identifier);
                                state = State.Default;
                            }
                            else throw new Exception();
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

            return result.ToString();
        }
    }
}