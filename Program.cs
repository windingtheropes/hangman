using System;
using System.Dynamic;
class Program {
    public static string[] bank = {"abandon", "ability", "accident", "address", "adventure", "agreement", "airplane", "alcohol", "alphabet", "ambulance", "animal", "apartment", "apple", "argument", "athlete", "atmosphere", "attraction", "audience", "author", "baby", "balance", "baseball", "basketball", "beach", "bedroom", "belief", "bicycle", "birthday", "blanket", "blossom", "boat", "boundary", "breakfast", "brother", "building", "butterfly", "calendar", "camera", "candle", "candy", "captain", "cardboard", "carpet", "celebration", "champion", "chocolate", "church", "cinema", "citizen", "coffee", "comfort", "community", "competition", "computer", "concert", "confusion", "connection", "conversation", "cooperation", "courage", "creation", "curiosity", "customer", "danger", "decision", "definition", "democracy", "dentist", "dessert", "direction", "discovery", "discussion", "distance", "diversity", "doctor", "education", "election", "electricity", "emotion", "employee", "energy", "environment", "equality", "equipment", "escape", "evening", "evolution", "examination", "experience", "experiment", "explanation", "family", "fascination", "father", "favorite", "festival", "fiction", "friendship", "furniture", "future", "garden", "generation", "geography", "girlfriend", "happiness", "history", "holiday", "humanity", "identity", "imagination", "improvement", "independence", "individual", "industry", "influence", "information", "initiative", "innovation", "inspiration", "instruction", "integrity", "intelligence", "interaction", "interest", "investment", "knowledge", "leadership", "learning", "library", "lifestyle", "literature", "loneliness", "loyalty", "magic", "marriage", "medicine", "memory", "midnight", "miracle", "mistake", "motivation", "music", "nature", "neighbor", "newspaper", "opportunity", "organization", "painting", "partnership", "passion", "patience", "peace", "performance", "personality", "photograph", "picture", "pleasure", "politics", "possibility", "president", "progress", "promise", "purpose", "quality", "question", "recognition", "relationship", "relaxation", "religion", "reminder", "research", "responsibility", "restaurant", "revelation", "satisfaction", "school", "science", "secret", "sensation", "service", "silence", "simplicity", "sister", "skill", "smile", "solution", "speech", "spirit", "stranger", "success", "suggestion", "summer", "surprise", "sympathy", "technology", "temptation", "tension", "tradition", "tranquility", "transportation", "treasure", "understanding", "universe", "vacation", "variety", "vegetable", "victory", "village", "vision", "volunteer", "wisdom", "wonder", "worker", "writing", "youth", "adventure", "ambition", "atmosphere", "audience", "authority", "awareness", "balance", "beauty", "beginning", "belief", "birthday", "brother", "building", "business", "butterfly", "camera", "capital", "celebration", "chance", "character", "charity", "childhood", "choice", "cinema", "climate", "coffee", "collaboration", "commitment", "communication", "compassion", "competition", "concept", "confidence", "connection", "consequence", "construction", "cooperation", "courage", "creation", "curiosity", "customer", "danger", "decision", "definition", "democracy", "department", "development", "direction", "discipline", "discovery", "discussion", "diversity", "economy", "education", "election", "emergency", "employee", "employment", "energy", "enjoyment", "environment", "equality", "equipment", "establishment", "examination", "experience", "explanation", "expression", "family", "fashion", "father", "feedback", "freedom", "friendship", "generation", "girlfriend", "government", "happiness", "harmony", "history", "holiday", "humanity", "identity", "imagination", "improvement", "independence", "individual", "industry", "influence", "information", "initiative", "innovation", "inspiration", "instruction", "integrity", "intelligence", "interaction", "interest", "investment", "knowledge", "leadership", "learning", "lifestyle", "literature", "love", "management", "marketing", "marriage", "meaning", "medicine", "memory", "motivation", "nature", "negotiation", "opportunity", "organization", "partnership", "passion", "patience", "performance", "perspective", "philosophy", "planning", "politics", "possibility", "power", "presence", "presentation", "progress", "promotion", "purpose", "quality", "reality", "recognition", "relationship", "relaxation", "religion", "reputation", "research", "responsibility", "restaurant", "satisfaction", "security", "selection", "sensation", "service", "significance", "simplicity", "sincerity", "society", "solution", "speaker", "spirit", "strategy", "structure", "success", "suggestion", "summer", "support", "surprise", "sympathy", "system", "technology", "tension", "tradition", "transformation", "transportation", "truth", "understanding", "universe", "vacation", "variety", "victory", "village", "violence", "vision", "wealth", "wedding", "welcome", "wisdom", "witness", "wonder", "worker", "writing", "adventure", "ambition", "atmosphere", "audience", "authority", "awareness", "balance", "beauty", "beginning", "belief", "birthday", "brother", "building", "business", "butterfly", "camera", "capital", "celebration", "chance", "character", "charity"};
    public static void Main(string[] args) {
        PreGame();
    }

    public static void PreGame() {
        Helper.ClearIn(0);
        Console.WriteLine("Welcome to hangman!");

        Helper.ClearIn();
        
        int gamemode = Helper.IntInput("Select the gamemode:\n(1) Singleplayer (2) Multiplayer", true, 1, false);
        if(gamemode == 2) {
            Helper.ClearIn(0);
            Console.WriteLine("2 player mode.");
            Helper.ClearIn();

            string word = Helper.Input("Player 2, enter the secret word: ");
            
            Helper.ClearIn(500);
            int limit = Helper.AskForDifficulty();

            Hangman h = new(limit, word);
            Helper.ClearIn(0);
            h.Start();
        } 
        else {
            Helper.ClearIn(0);
            Console.WriteLine("Single player mode.");

            Helper.ClearIn(500);
            int limit = Helper.AskForDifficulty();

            Random random = new();
            Hangman h = new(limit, bank[random.Next(0, bank.Length)]);
            h.Start();
        }
    }
}


class Colours {
    public static string reset = "\x1B[0m";
    public static string bold = "\x1B[1m";
    public static string blue = "\x1B[34m";
    public static string red = "\x1B[31m";
    public static string yellow = "\x1B[33m";
    public static string green = "\x1B[32m";
}

class Helper {
    public static string alphabet = "abcdefghijklmnopqrstuvwxyz";

    public static void ClearIn(int duration = 1500) {
        Thread.Sleep(duration);
        Console.Clear();
    }
    
    public static int AskForDifficulty() {
        int difficulty = IntInput("Enter the difficulty:\n(1) Easy\n(2) Medium\n(3) Hard\n(4) Impossible", true, 2, false);
        string[] difficultyNames = {"easy", "medium", "hard", "impossible"};
        int[] difficultyLimits = {10, 8, 6, 4};

        ClearIn(0);
        Console.WriteLine($"Difficulty set to {difficultyNames[difficulty-1]}.");
        
        return difficultyLimits[difficulty-1];
    }

    // Add a string, 'a', to another string, 'b', without double letters
    public static string ExclusiveAdd(string a, string b) {
        foreach(char aC in a.ToCharArray()){
            if(b.ToCharArray().Contains(aC)) continue;
            else b = $"{b}{aC}";
        }
        return b;
    }
    public static int IntInput(string prompt, bool newline, int d = -1, bool forceAns = false) {
        ClearIn(500);
        string stringInput = Input(prompt, newline, forceAns);
        int integerInput = d;
        try {
            integerInput = int.Parse(stringInput);
        }
        catch {
            if(forceAns == true) {
                Console.WriteLine("Input is not an integer.");
                IntInput(prompt, newline, d, forceAns);
            }
            
        }
        return integerInput;
    }

    // Ask for input with a prompt, and return it as a string
    public static string Input(string prompt, bool newline = false, bool forceAns = true) {
        if(newline == true) {
            Console.WriteLine(prompt);
        }
        else {
            Console.Write(prompt);
        }
        string? userInput = Console.ReadLine();
        if((userInput == "" || userInput == null) && forceAns == true) return Input(prompt, newline, forceAns);
        else return userInput;
    }
}
class Hangman {
    private string lettersGuessed = "";
    private int guessesRemaining;
    private int guesses;
    private string word = string.Empty;

    public Hangman(int g, string w) {
        guesses = g;
        guessesRemaining = guesses;
        word = w;
    }
    private string FormatLettersGuessed(string lettersGuessed) {
        string formattedString = "";
        foreach(char letter in Helper.alphabet.ToCharArray()) {
            if(lettersGuessed.ToCharArray().Contains(letter)) {
                if(word.ToCharArray().Contains(letter)) {
                    formattedString = $"{formattedString} {Colours.bold}{Colours.green}({letter}){Colours.reset}";
                }
                else {
                    formattedString = $"{formattedString} {Colours.bold}{Colours.red}({letter}){Colours.reset}";
                }
            }
            else {
                formattedString = $"{formattedString} {letter}";
            }
        }
        return formattedString;
    }
    private bool CheckWord() {
        string guessWord = "";
        foreach(char i in word.ToCharArray()) {
                if(lettersGuessed.ToCharArray().Contains(i) == true) {
                        guessWord = $"{guessWord}{i}";
                }
                else {
                        guessWord = $"{guessWord}_";
                }
        }
        if(guessWord == word) return true;
        else return false;
    }

    private void ShowHint() {
        string hintWord = string.Empty;
        Console.WriteLine(FormatLettersGuessed(lettersGuessed));
        foreach(char wordChar in word.ToCharArray()) {
            if(Helper.alphabet.ToCharArray().Contains(wordChar) == true) {
                // Check if the wordChar has been guessed
                if(lettersGuessed.ToCharArray().Contains(wordChar) == true) {
                    hintWord = $"{hintWord}{wordChar}";
                }
                else {
                    hintWord = $"{hintWord}_";
                }
            }
            else {
                // Ignore non alphabet characters, add them to the hint regardless
                hintWord = $"{hintWord}{wordChar}";
            }
        }
        Console.WriteLine();

        string progressBar = "";
        for(int i = 0; i < guesses; i++) {
            if(i < guessesRemaining) {
                progressBar = $"{progressBar}{Colours.green}{Colours.bold}o{Colours.reset}";
            }
            else {
                progressBar = $"{progressBar}{Colours.red}{Colours.bold}o{Colours.reset}";
            }
        }

        Console.WriteLine($"{progressBar}\n");
        Console.WriteLine($"{Colours.bold}{hintWord}{Colours.reset}");
    }

    private void ProcessGuess() {
        // Ask the user for their guess, will take the first character if multiple are given, or the whole word if it's guessed correctly
        string stringGuess = Helper.Input("Enter your letter guess: ", false).ToLower();
        if(stringGuess.Length > 1) {
            // Amalgomate the word and lettersguessed, showing that the user has guessed the word successfully
            if(stringGuess == word) {
                lettersGuessed = Helper.ExclusiveAdd(stringGuess, lettersGuessed);
                return;
            }
            else {
                Console.WriteLine("That's not the word!");
                guessesRemaining -= 3;
                Helper.ClearIn(500);
                return;
            }
            
        }
        char guess = stringGuess.ToLower().ToCharArray()[0];
        // If guess is not in the alphabet, ask for it again
        if(Helper.alphabet.ToCharArray().Contains(guess)) {
            // Check whether or not the letter has been guessed already, if not, add it to lettersguessed
            if(lettersGuessed.ToCharArray().Contains(guess) == false) {
                lettersGuessed = $"{lettersGuessed}{guess}";
                // Check whether or not the guessed character is in the word; notify the user. Subtract from the guesses remaining if the guess is incorrect.
                if(word.ToCharArray().Contains(guess)) {
                    Helper.ClearIn(0);
                    Console.WriteLine($"{guess} is in the word!");
                }
                else {
                    guessesRemaining -= 1;
                    Helper.ClearIn(0);
                    Console.WriteLine($"{guess} is not in the word.");
                }
            }
            else {
                Helper.ClearIn(0);
                Console.WriteLine($"{guess} has already been guessed.");
                ProcessGuess();
            }
        }
        else {
            Helper.ClearIn(0);
            Console.WriteLine("Guess is not a letter.");
            ProcessGuess();
        }
        Helper.ClearIn(500);
    }

    private void Welcome() {
        Helper.ClearIn(1500);

        Console.WriteLine("Welcome to hangman!");
        Console.WriteLine("Try to guess the secret word!");
        
        Helper.ClearIn(1500);

        ShowHint();
    }

    private void PlayAgain() {
        Helper.ClearIn();

        bool yes = Helper.Input("Would you like to play again? (y/n) ", false).ToLower() == "y";
        if(yes) {
            Helper.ClearIn(0);
            Program.PreGame();
        }
        else {
            Helper.ClearIn(0);
            Console.WriteLine("Goodbye.");
        }
    }
    public void Start() {
        Welcome();
        
        while (guessesRemaining > 0 && CheckWord() == false) {
            ProcessGuess();
            ShowHint();
        }

        if(guessesRemaining == 0) {
            Console.WriteLine("You ran out of guesses!");
        }
        if(CheckWord() == true) {
            Console.WriteLine("You guessed the word!");
        }

        PlayAgain();
    }
}