using System;
using System.Collections.Generic;

public class QuizManager
{
    private List<QuizQuestion> _questions = new List<QuizQuestion>();
    private int _currentIndex = 0;
    private int _score = 0;

    public QuizManager()
    {
        PopulateQuestions();
    }

    // this is where the questions are being populated 
    private void PopulateQuestions()
    {
        _questions.Add(new QuizQuestion
        {
            Question = "What should you do if you receive an email asking for your password?",
            Options = new List<string> { "A) Reply with your password", "B) Delete the email", "C) Report as phishing", "D) Ignore it" },
            CorrectAnswer = "C",
            Explanation = "Correct! Reporting phishing emails helps prevent scams."
        });

        _questions.Add(new QuizQuestion
        {
            Question = "Which is the strongest password practice?",
            Options = new List<string> { "A) Using 'Password123'", "B) Using your birthday", "C) Using a unique passphrase with numbers and symbols", "D) Reusing the same password everywhere" },
            CorrectAnswer = "C",
            Explanation = "Strong, unique passwords are essential for account security."
        });

        _questions.Add(new QuizQuestion
        {
            Question = "True or False: You should click on links in unsolicited emails.",
            Options = new List<string> { "A) True", "B) False" },
            CorrectAnswer = "B",
            Explanation = "Never click links in suspicious emails – it could be phishing.",
            IsTrueFalse = true
        });

        _questions.Add(new QuizQuestion
        {
            Question = "What is the best way to protect your privacy on social media?",
            Options = new List<string> { "A) Share everything publicly", "B) Use strong privacy settings and limit personal info", "C) Accept all friend requests", "D) Post your location daily" },
            CorrectAnswer = "B",
            Explanation = "Good privacy settings reduce the risk of identity theft and social engineering."
        });

        _questions.Add(new QuizQuestion
        {
            Question = "What is malware?",
            Options = new List<string> { "A) A type of firewall", "B) Malicious software designed to harm your device", "C) A security update", "D) An encryption tool" },
            CorrectAnswer = "B",
            Explanation = "Malware includes viruses, ransomware, trojans, etc."
        });

        _questions.Add(new QuizQuestion
        {
            Question = "When browsing, you should always look for ___ in the URL.",
            Options = new List<string> { "A) http://", "B) https:// and the padlock icon", "C) Long domain names", "D) Lots of pop-ups" },
            CorrectAnswer = "B",
            Explanation = "HTTPS ensures encrypted and secure communication."
        });

        _questions.Add(new QuizQuestion
        {
            Question = "What is social engineering?",
            Options = new List<string> { "A) Hacking into networks", "B) Manipulating people into revealing confidential information", "C) Installing antivirus software", "D) Creating strong passwords" },
            CorrectAnswer = "B",
            Explanation = "Social engineering attacks exploit human psychology rather than technical vulnerabilities."
        });

        _questions.Add(new QuizQuestion
        {
            Question = "Why is it important to backup your data?",
            Options = new List<string> { "A) To free up space", "B) To recover data after ransomware or hardware failure", "C) To make your computer faster", "D) To share files easily" },
            CorrectAnswer = "B",
            Explanation = "Regular backups protect against data loss from attacks or failures."
        });

        _questions.Add(new QuizQuestion
        {
            Question = "What does 2FA (Two-Factor Authentication) add to your login?",
            Options = new List<string> { "A) Nothing", "B) A second verification step (e.g. code on phone)", "C) A new password", "D) Faster login" },
            CorrectAnswer = "B",
            Explanation = "2FA greatly increases account security."
        });

        _questions.Add(new QuizQuestion
        {
            Question = "What is the purpose of encryption?",
            Options = new List<string> { "A) To make files bigger", "B) To convert data into a coded form that only authorized users can read", "C) To delete old files", "D) To speed up internet" },
            CorrectAnswer = "B",
            Explanation = "Encryption protects sensitive data from unauthorized access."
        });

        _questions.Add(new QuizQuestion
        {
            Question = "What does a firewall do?",
            Options = new List<string> { "A) Cleans your screen", "B) Monitors and controls incoming and outgoing network traffic", "C) Stores passwords", "D) Creates backups" },
            CorrectAnswer = "B",
            Explanation = "A firewall is a key defense against unauthorized network access."
        });

        _questions.Add(new QuizQuestion
        {
            Question = "Why should you install security updates regularly?",
            Options = new List<string> { "A) To make your computer look nice", "B) To patch security vulnerabilities and protect against new threats", "C) To use more internet data", "D) To change the wallpaper" },
            CorrectAnswer = "B",
            Explanation = "Updates fix known security holes that attackers exploit."
        });

        _questions.Add(new QuizQuestion
        {
            Question = "True or False: It is safe to use public Wi-Fi without a VPN for banking.",
            Options = new List<string> { "A) True", "B) False" },
            CorrectAnswer = "B",
            Explanation = "Public Wi-Fi is risky. Always use a VPN for sensitive activities.",
            IsTrueFalse = true
        });

        // Bonus question (Total 13 questions)
        _questions.Add(new QuizQuestion
        {
            Question = "What is a common sign of a scam email?",
            Options = new List<string> { "A) Urgent language and threats", "B) Professional company logo", "C) Correct grammar", "D) Personal greeting" },
            CorrectAnswer = "A",
            Explanation = "Urgency and threats are classic scam tactics."
        });
    }

    public QuizQuestion GetCurrentQuestion() => _questions[_currentIndex];

    public bool SubmitAnswer(string answer)
    {
        bool correct = answer.Trim().Equals(GetCurrentQuestion().CorrectAnswer, StringComparison.OrdinalIgnoreCase);
        if (correct) _score++;
        _currentIndex++;
        return correct;
    }

    public bool IsFinished() => _currentIndex >= _questions.Count;

    public string GetFeedback(bool correct)
    {
        return GetCurrentQuestion().Explanation;
    }

    public string GetFinalScore() => $"Your score: {_score} out of {_questions.Count}";
    public string GetFinalMessage()
    {
        return _score >= 10 ? "Excellent! You have strong cybersecurity knowledge."
                           : _score >= 7 ? "Good job! Keep improving."
                           : "Keep learning! Cybersecurity awareness is very important.";
    }

    public void Reset()
    {
        _currentIndex = 0;
        _score = 0;
    }
}