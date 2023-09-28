using System;
class Program {
    public static void Main(string[] args) {
        Hangman hangman = new(5);
        hangman.Start();
    }
}

class Helper {
    public static string alphabet = "abcdefghijklmnopqrstuvwxyz";
    // Add a string, 'a', to another string, 'b', without double letters
    public static string ExclusiveAdd(string a, string b) {
        foreach(char aChar in a.ToCharArray()) {
            if(findCharInString(b, aChar)) continue;
            else if(findCharInString(alphabet, aChar)) b = $"{b}{aChar}";
            else continue;
        }
        return b;
    }
    // Ask for input with a prompt, and return it as a string
    public static string Input(string prompt, bool newline) {
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

    // Test if a character 'i' is present in string 's'
    public static bool findCharInString(string s, char i) {
        foreach(char c in s.ToCharArray()) {
            if(c == i) {
                return true;
            }
        }
        return false;
    }
}
class Hangman {
    private string lettersGuessed = string.Empty;
    private int guessesRemaining;
    private int guesses;
    private string word = string.Empty;
    private string[] bank = {"hello", "arcade", "computer"};

    public Hangman(int g) {
        Random random = new();

        guesses = g;
        guessesRemaining = guesses;
        word = bank[random.Next(0,bank.Length)];;
    }

    private bool checkWord() {
        string guessWord = "";
        foreach(char i in word.ToCharArray()) {
                if(Helper.findCharInString(lettersGuessed, i) == true) {
                        guessWord = $"{guessWord}{i}";
                }
                else {
                        guessWord = $"{guessWord}_";
                }
        }
        if(guessWord == word) return true;
        else return false;
    }

    private void showHint() {
        string hintWord = string.Empty;

        Console.WriteLine($"Letters guessed: {lettersGuessed}");
        foreach(char wordChar in word.ToCharArray()) {
            if(Helper.findCharInString(Helper.alphabet, wordChar) == true) {
                // Check if the wordChar has been guessed
                if(Helper.findCharInString(lettersGuessed, wordChar) == true) {
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

    private void processGuess() {
        // Ask the user for their guess, will take the first character if multiple are given, or the whole word if it's guessed correctly
        string stringGuess = Helper.Input("Enter your letter guess: ", false).ToLower();
        if(stringGuess == word) {
            // Amalgomate the word and lettersguessed, showing that the user has guessed the word successfully
            lettersGuessed = Helper.ExclusiveAdd(stringGuess, lettersGuessed);
            return;
        }
        char guess = stringGuess.ToLower().ToCharArray()[0];
        // If guess is not in the alphabet, ask for it again
        if(Helper.findCharInString(Helper.alphabet, guess)) {
            // Check whether or not the letter has been guessed already, if not, add it to lettersguessed
            if(Helper.findCharInString(lettersGuessed, guess) == false) {
                lettersGuessed = $"{lettersGuessed}{guess}";
                // Check whether or not the guessed character is in the word; notify the user. Subtract from the guesses remaining if the guess is incorrect.
                if(Helper.findCharInString(word, guess)) {
                    Console.WriteLine($"{guess} is in the word!");
                }
                else {
                    guessesRemaining -= 1;
                    Console.WriteLine($"{guess} is not in the word...");
                }
            }
            else {
                Console.WriteLine($"{guess} has already been guessed.");
                processGuess();
            }
        }
        else {
            Console.WriteLine("Guess is not a letter.");
            processGuess();
        }
        
    }

    private void welcome() {
        Console.WriteLine("Welcome to hangman!");
        Console.WriteLine("Try to guess the secret word! \n");
        showHint();
    }
    public void Start() {
        welcome();
        while (guessesRemaining > 0 && checkWord() == false) {
            processGuess();
            showHint();
        }
        if(guessesRemaining == 0) {
            Console.WriteLine("You ran out of guesses!");
        }
        if(checkWord() == true) {
            Console.WriteLine("You guessed the word!");
        }
    }
}