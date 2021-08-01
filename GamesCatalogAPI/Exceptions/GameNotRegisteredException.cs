using System;

namespace GamesCatalogAPI.Exceptions {
    public class GameNotRegisteredException : Exception {
        public GameNotRegisteredException()
        : base("This game is not registered yet!") {
        }
    }
}
