using System;

namespace GamesCatalogAPI.Exceptions {
    public class GameAlreadyRegisteredException : Exception {
        public GameAlreadyRegisteredException()
            : base("This game is already resgistered!") {
        }

    }
}
