using System;
using System.ComponentModel;
class Program {
    public static void Main() {
        Console.WriteLine("Welcome to hangman!");
        
        int limit = Helper.IntInput("Enter the guess limit: ", false, 5);
        Console.WriteLine($"Limit of guesses is now set to {limit}.");
        
        int gamemode = Helper.IntInput("Select the gamemode:\n(1) Singleplayer (2) Multiplayer", true, 1);
        if(gamemode == 2) {
            string word = Helper.Input("Player 2, enter the secret word: ", false);
            Hangman h = new(limit, word);
            h.Start();
        } 
        else {
            string[] bank = {"hello", "arcade", "computer"};
            Random random = new();
            Hangman h = new(limit, bank[random.Next(0, bank.Length)]);
            h.Start();
        }
    }
}

class Helper {
    public static string alphabet = "abcdefghijklmnopqrstuvwxyz";

    public static string FormatLettersGuessed(string lettersGuessed) {
        string formattedString = "";
        foreach(char letter in alphabet.ToCharArray()) {
            if(lettersGuessed.ToCharArray().Contains(letter)) {
                formattedString = $"{formattedString} ({letter})";
            }
            else {
                formattedString = $"{formattedString} {letter}";
            }
        }
        return formattedString;
    }
    // Add a string, 'a', to another string, 'b', without double letters
    public static string ExclusiveAdd(string a, string b) {
        foreach(char aC in a.ToCharArray()){
            if(b.ToCharArray().Contains(aC)) continue;
            else b = $"{b}{aC}";
        }
        return b;
    }
    public static int IntInput(string prompt, bool newline, int d = -1) {
        string? stringInput = Input(prompt, newline);
        int integerInput = d;
        try {
            integerInput = int.Parse(stringInput);
        }
        catch {
            Console.WriteLine("Input is not an integer.");
            IntInput(prompt, newline, d);
        }
        return integerInput;
    }

    // Ask for input with a prompt, and return it as a string
    public static string Input(string prompt, bool newline = false) {
        if(newline == true) {
            Console.WriteLine(prompt);
        }
        else {
            Console.Write(prompt);
        }
        string? userInput = Console.ReadLine();
        if(userInput == "" || userInput == null) return Input(prompt, newline);
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
        Console.WriteLine(Helper.FormatLettersGuessed(lettersGuessed));
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
        Console.WriteLine(hintWord);
    }

    private void ProcessGuess() {
        // Ask the user for their guess, will take the first character if multiple are given, or the whole word if it's guessed correctly
        string stringGuess = Helper.Input("Enter your letter guess: ", false).ToLower();
        if(stringGuess == word) {
            // Amalgomate the word and lettersguessed, showing that the user has guessed the word successfully
            lettersGuessed = Helper.ExclusiveAdd(stringGuess, lettersGuessed);
            return;
        }
        char guess = stringGuess.ToLower().ToCharArray()[0];
        // If guess is not in the alphabet, ask for it again
        if(Helper.alphabet.ToCharArray().Contains(guess)) {
            // Check whether or not the letter has been guessed already, if not, add it to lettersguessed
            if(lettersGuessed.ToCharArray().Contains(guess) == false) {
                lettersGuessed = $"{lettersGuessed}{guess}";
                // Check whether or not the guessed character is in the word; notify the user. Subtract from the guesses remaining if the guess is incorrect.
                if(word.ToCharArray().Contains(guess)) {
                    Console.WriteLine($"{guess} is in the word!");
                }
                else {
                    guessesRemaining -= 1;
                    Console.WriteLine($"{guess} is not in the word...");
                }
            }
            else {
                Console.WriteLine($"{guess} has already been guessed.");
                ProcessGuess();
            }
        }
        else {
            Console.WriteLine("Guess is not a letter.");
            ProcessGuess();
        }
        Console.Clear();
    }

    private void Welcome() {
        Console.Clear();
        Console.WriteLine("Welcome to hangman!");
        Console.WriteLine("Try to guess the secret word!");
        Thread.Sleep(1000);
        Console.Clear();

        ShowHint();
    }

    private void PlayAgain() {
        Thread.Sleep(2000);
        Console.Clear();

        bool yes = Helper.Input("Would you like to play again? (y/n) ", false).ToLower() == "y";
        if(yes) {
            Console.Clear();
            Program.Main();
        }
        else {
            Console.Clear();
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