public class QuizQuestion
{
    // this is the class that will be used to store my quiz questions and answers 
    public string Question { get; set; } = string.Empty;
    public List<string> Options { get; set; } = new List<string>();
    public string CorrectAnswer { get; set; } = string.Empty;
    public string Explanation { get; set; } = string.Empty;
    public bool IsTrueFalse { get; set; } = false;
}