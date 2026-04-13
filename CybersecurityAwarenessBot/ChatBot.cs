using System;
using System.Runtime.InteropServices;
using System.IO;

namespace CybersecurityAwarenessBot
{
    class ChatBot
    {
        private string userName = "";
        private Random random = new Random();

        // Import Windows PlaySound API (no package needed)
        [DllImport("winmm.dll")]
        private static extern bool PlaySound(string pszSound, IntPtr hmod, int fdwSound);

        // Feature 1: Voice Greeting with clear feedback
        public void PlayVoiceGreeting()
        {
            try
            {
                // Check if greeting.wav exists
                string wavPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "greeting.wav");

                if (File.Exists(wavPath))
                {
                    // Try to play the sound
                    bool played = PlaySound(wavPath, IntPtr.Zero, 0x0001); // SND_ASYNC | SND_FILENAME

                    if (played)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("🔊 Voice greeting playing...");
                        Console.ResetColor();
                        System.Threading.Thread.Sleep(2000); // Give time for audio to play
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("⚠️  Voice file found but could not be played.");
                        Console.ResetColor();
                    }
                }
                else
                {
                    // Voice file not found - show message
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("🔇 Voice greeting not found (greeting.wav missing)");
                    Console.WriteLine("💡 To enable voice: Place a 'greeting.wav' file in:");
                    Console.WriteLine($"   {AppDomain.CurrentDomain.BaseDirectory}");
                    Console.ResetColor();
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"❌ Voice error: {ex.Message}");
                Console.ResetColor();
            }
        }

        // Feature 2: Display ASCII Logo
        public void DisplayASCIILogo()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            string asciiArt = @"
    ╔══════════════════════════════════════════════════════════════════╗
    ║                         CYBERSECURITY                            ║
    ║                      AWARENESS BOT v1.0                          ║
    ╠══════════════════════════════════════════════════════════════════╣
    ║                                                                  ║
    ║         ██████╗██╗   ██╗██████╗ ███████╗██████╗                   ║
    ║        ██╔════╝╚██╗ ██╔╝██╔══██╗██╔════╝██╔══██╗                  ║
    ║        ██║      ╚████╔╝ ██████╔╝█████╗  ██████╔╝                  ║
    ║        ██║       ╚██╔╝  ██╔══██╗██╔══╝  ██╔══██╗                  ║
    ║        ╚██████╗   ██║   ██████╔╝███████╗██║  ██║                  ║
    ║         ╚═════╝   ╚═╝   ╚═════╝ ╚══════╝╚═╝  ╚═╝                  ║
    ║                                                                  ║
    ║                   STAY SAFE • STAY SECURE                        ║
    ║                                                                  ║
    ║              [ PASSWORD SAFETY | PHISHING | BROWSING ]           ║
    ║                                                                  ║
    ╚══════════════════════════════════════════════════════════════════╝
            ";
            Console.WriteLine(asciiArt);
            Console.ResetColor();
            Console.WriteLine();
        }

        // Feature 3: Get User Name and Personalize
        public void GetUserNameAndGreet()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("🔐 Please enter your name: ");
            Console.ResetColor();

            userName = Console.ReadLine()?.Trim();

            while (string.IsNullOrWhiteSpace(userName))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("⚠️  Name cannot be empty. Please enter your name: ");
                Console.ResetColor();
                userName = Console.ReadLine()?.Trim();
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n✨ Welcome, {userName}! ✨");
            Console.WriteLine($"🤖 I'm your Cybersecurity Awareness Bot.");
            Console.WriteLine($"💬 I'll help you learn about staying safe online.\n");
            Console.ResetColor();
        }

        // Feature 4 & 5: Response System with Input Validation
        public void StartChat()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("╔════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║  You can ask me about:                                     ║");
            Console.WriteLine("║  • Password safety                                        ║");
            Console.WriteLine("║  • Phishing attacks                                       ║");
            Console.WriteLine("║  • Safe browsing                                          ║");
            Console.WriteLine("║  • General questions like 'How are you?'                  ║");
            Console.WriteLine("║  • Type 'exit' or 'quit' to leave                         ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════╝\n");
            Console.ResetColor();

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"{userName} > ");
                Console.ResetColor();

                string userInput = Console.ReadLine()?.Trim().ToLower();

                // Feature 5: Empty input validation
                if (string.IsNullOrWhiteSpace(userInput))
                {
                    HandleInvalidInput();
                    continue;
                }

                // Exit condition
                if (userInput == "exit" || userInput == "quit" || userInput == "bye")
                {
                    SayGoodbye();
                    break;
                }

                // Feature 4: Get response
                string response = GetResponse(userInput);
                DisplayResponse(response);
            }
        }

        private string GetResponse(string input)
        {
            // Greeting responses
            if (input.Contains("how are you"))
                return "I'm functioning securely! Thanks for asking. How can I help you with cybersecurity today?";

            if (input.Contains("purpose") || input.Contains("what can you do"))
                return "My purpose is to educate you about cybersecurity. You can ask me about:\n" +
                       "  • Password safety tips\n" +
                       "  • How to recognize phishing emails\n" +
                       "  • Safe browsing habits";

            if (input.Contains("what can i ask"))
                return "You can ask me about passwords, phishing, safe browsing, or just say 'help' for options!";

            if (input.Contains("your name") || input.Contains("who are you"))
                return $"I'm the Cybersecurity Awareness Bot, your digital guardian {userName}!";

            // Password safety
            if (input.Contains("password") || input.Contains("passphrase") || input.Contains("pwd"))
            {
                return "🔒 PASSWORD SAFETY TIPS:\n" +
                       "  • Use long passphrases (12+ characters)\n" +
                       "  • Never reuse passwords across sites\n" +
                       "  • Enable 2FA (Two-Factor Authentication)\n" +
                       "  • Use a password manager like Bitwarden or LastPass\n" +
                       "  • Change passwords if you suspect a breach";
            }

            // Phishing
            if (input.Contains("phish") || input.Contains("scam") || input.Contains("fraud") || input.Contains("email"))
            {
                return "🎣 PHISHING WARNING SIGNS:\n" +
                       "  • Urgent language ('Act now or your account will be closed!')\n" +
                       "  • Suspicious sender email addresses\n" +
                       "  • Generic greetings ('Dear Customer' instead of your name)\n" +
                       "  • Spelling and grammar mistakes\n" +
                       "  • Hover over links to see the real URL\n" +
                       "  • Requests for passwords, credit cards, or personal info\n" +
                       "  → When in doubt, contact the company directly using official channels!";
            }

            // Safe browsing
            if (input.Contains("brows") || input.Contains("internet") || input.Contains("web") || input.Contains("online") || input.Contains("safe"))
            {
                return "🌐 SAFE BROWSING HABITS:\n" +
                       "  • Look for 'https://' and padlock icon in address bar\n" +
                       "  • Don't download files from untrusted sources\n" +
                       "  • Keep your browser and extensions updated\n" +
                       "  • Use ad-blockers (uBlock Origin) and script blockers\n" +
                       "  • Clear cookies and cache regularly\n" +
                       "  • Avoid public Wi-Fi for banking or sensitive transactions\n" +
                       "  • Use a VPN on untrusted networks";
            }

            // Help menu
            if (input.Contains("help") || input.Contains("menu") || input.Contains("options"))
            {
                return "📋 TOPICS I CAN HELP WITH:\n" +
                       "  • Password safety - tips for strong passwords\n" +
                       "  • Phishing scams - how to spot fake emails/messages\n" +
                       "  • Safe browsing - secure internet habits\n" +
                       "  • Just type 'exit' when you're done!\n\n" +
                       "Try asking: 'Tell me about phishing' or 'Password tips'";
            }

            // Thanks
            if (input.Contains("thank") || input.Contains("thanks") || input.Contains("good bot"))
            {
                return $"You're welcome, {userName}! Stay safe online! 🛡️ Remember: cybersecurity is everyone's responsibility!";
            }

            // Feature 5: Default for unrecognized input
            return GetDefaultResponse();
        }

        private string GetDefaultResponse()
        {
            string[] fallbacks = {
                "I didn't quite understand that. Could you rephrase?",
                "Hmm, I'm not sure what you mean. Try asking about password safety, phishing, or safe browsing!",
                "I specialize in cybersecurity topics. Would you like tips on passwords, phishing, or safe browsing?",
                "Sorry, I didn't catch that. Type 'help' to see what I can do!",
                $"I'm a cybersecurity bot, {userName}. I can teach you about online safety. Ask me about passwords!"
            };

            return fallbacks[random.Next(fallbacks.Length)];
        }

        private void HandleInvalidInput()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n⚠️  I didn't receive any input. Please type a question or 'help' for options.\n");
            Console.ResetColor();
        }

        private void DisplayResponse(string response)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n🤖 Bot: {response}\n");
            Console.ResetColor();
        }

        private void SayGoodbye()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n👋 Goodbye, {userName}! Remember these 3 rules:");
            Console.WriteLine("   1. Use strong, unique passwords");
            Console.WriteLine("   2. Think before you click");
            Console.WriteLine("   3. Keep software updated");
            Console.WriteLine("\n   🔐 Stay safe, stay secure! 🔐\n");
            Console.ResetColor();
        }
    }
}