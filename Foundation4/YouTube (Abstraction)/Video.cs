public class Video
{
    private string _title;
    private string _author;
    private int _lengthSeconds;
    private List<Comment> _comments = new List<Comment>();

    public Video(string title, string author, int lengthSeconds)
    {
        _title = title;
        _author = author;
        _lengthSeconds = lengthSeconds;
    }

    public void AddComment(Comment comment)
    {
        _comments.Add(comment);
    }

    public int GetCommentCount()
    {
        return _comments.Count;
    }

    public string GetDisplayText()
    {
        string text = $"Title: {_title}\nAuthor: {_author}\nLength: {_lengthSeconds} seconds\nComments ({GetCommentCount()}):";
        foreach (Comment comment in _comments)
        {
            text += "\n" + comment.GetDisplayText();
        }
        return text;
    }
}
