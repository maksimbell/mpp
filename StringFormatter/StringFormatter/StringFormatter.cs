using StringFormatting;
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

        private State _state;
        public string Format(string template, object target)
        {
            StringBuilder current = new StringBuilder();
            StringBuilder result = new StringBuilder();
            _state = State.Default;

            for(int i = 0; i < template.Length; i++)
            {
                switch(_state)
                {
                    case State.Default:
                        if(template[i] == '{')
                        {
                            result.Append(template[i]);
                            _state = State.Open;
                        } else if(template[i] == '}')
                        {
                            result.Append(template[i]);
                            _state = State.Close;
                        }
                        else
                        {
                            result.Append(template[i]);
                            _state = State.Default;
                        }
                        break;
                    case State.Open:
                        if(_identifierChars.Contains(template[i]))//identifier char
                        {
                            result.Remove(result.Length - 1, 1);
                            current.Clear();
                            current.Append(template[i]);
                            _state = State.Character;
                        }
                        else if(template[i] == '{')
                        {
                            _state = State.Default;
                        }
                        else throw new FormatterException("Invalid identifier start");
                        break;
                    case State.Character:
                        if(_identifierChars.Contains(template[i]) ||
                            _digits.Contains(template[i]))
                        {
                            current.Append(template[i]);
                            _state = State.Character;
                        }
                        else if(template[i] == '}')
                        {
                            string? identifier = _cache.GetOrAdd(current.ToString(), target);
                            if(identifier != null)
                            {
                                result.Append(identifier);
                                _state = State.Default;
                            }
                            else throw new FormatterException("No such class member");
                        }
                        else throw new FormatterException("Invalid identifier body");
                        break;
                    case State.Close:
                        if(template[i] == '}')
                        {
                            _state = State.Default;
                        }
                        else throw new FormatterException("Not enough curly braces");
                        break;
                }
            }

            if(State.Default != _state)
                throw new FormatterException("Not enough curly braces");

            return result.ToString();
        }
    }
}