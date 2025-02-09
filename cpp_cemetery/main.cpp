/*
  SDL example that allows user to move an object using arrow keys.
  This is meant to be used as a convenient single-file starting point for
  more complex projects.

  Tested with Code::Blocks on Windows 7. You will need the SDL files.
  If building with GCC/MinGW, use these linker options:
  -lmingw32
  -lSDLmain
  -lSDL

  Author: Andrew Lim Chong Liang
  windrealm.org
*/

#include <grafika/grafika.h>

int main(int argc, char** argv)
{
    Obrazovka* obrazovka = Obrazovka::instance();
    obrazovka->inicializuj(800, 600, 0, 0);

    while (1) {
        obrazovka->smaz();

        /* zacatek kresleni */
        barva(BILA);
        cara(0, 0, 100, 100);

        /* konec kresleni */
        obrazovka->aktualizuj();

        SDL_Event event;
        while (SDL_PollEvent(&event)) {
            switch (event.type) {
            case SDL_KEYDOWN:
                switch (event.key.keysym.sym) {
                case SDLK_ESCAPE:
                    SDL_Quit();
                    return 0;
                }
                break;
            }
        }
    }
}
