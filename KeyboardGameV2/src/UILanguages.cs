// these are all the text labels in the user interface
// a complete object is required for a full translation
namespace KeyboardGameV2.src
{
    public class UILanguages
    {
        public static readonly UILanguage EN =
            new("Not Scrabble or Boggle", "Game Over!", "please press a letter", "Player {0}",
                new("Load Dictionary","Start Game", "Stop Game", "Players",
                    "Assign Player {0}", "Release Player {0}",
                    new("Options", "Game Timer (seconds)",
                        "Pool Letter Count",
                        "Show Words Length X or Longer After Game",
                        new("Letter Pool Format", "Sorted", "Points", "Spaces"),
                        new("Letter Mode", "Dictionary", "Bag"))));

        public static readonly UILanguage ES =
            new("Necesito un Mejor Nombre Para Esto", "¡Juego Terminado!", "por favor presione una letra", "Jugador {0}",
                new("Diccionario de Carga", "Iniciar Juego", "Detener el Juego", "Jugadores",
                    "Asignar Jugador {0}", "Liberar Jugador {0}",
                    new("Opciones", "Temporizador de Juego (segundos)",
                        "Recuento de Letras de la Piscina",
                        "Mostrar Palabras de Longitud X o Mayor Después del Juego",
                        new("Formato de Letras de la Piscina", "Ordenado", "Agujas", "Espacios"),
                        new("Modo Letra", "Diccionario", "Bolsa"))));

        public class UILanguage(string title, string gameOver, string assign, string boxes, UILanguage.Menu menu)
        {
            public readonly string title = title;
            public readonly string gameOver = gameOver;
            public readonly string assign = assign;
            public readonly string boxes = boxes;
            public readonly Menu menu = menu;

            public class Menu(string load, string start, string stop, string players,
                string assign, string release, Menu.Options options)
            {
                public readonly string load = load;
                public readonly string start = start;
                public readonly string stop = stop;
                public readonly string players = players;
                public readonly string assign = assign;
                public readonly string release = release;
                public readonly Options options = options;

                public class Options(string self, string timer, string count, string show,
                    Options.LetterPoolFormat letterPoolFormat, Options.LetterMode letterMode)
                {
                    public readonly string self = self;
                    public readonly string timer = timer;
                    public readonly string count = count;
                    public readonly string show = show;
                    public readonly LetterPoolFormat letterPoolFormat = letterPoolFormat;
                    public readonly LetterMode letterMode = letterMode;

                    public class LetterPoolFormat(string self, string sorted, string points, string spaces)
                    {
                        public readonly string self = self;
                        public readonly string sorted = sorted;
                        public readonly string points = points;
                        public readonly string spaces = spaces;
                    }

                    public class LetterMode(string self, string dictionary, string bag)
                    {
                        public readonly string self = self;
                        public readonly string dictionary = dictionary;
                        public readonly string bag = bag;
                    }
                }
            }
        }
    }
}
