namespace SkypeMessageSearch
{
    public class SkypeContact
    {
        public string SkypeName { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return string.Format("{0} ({1})", Name, SkypeName);
        }
    }
}
