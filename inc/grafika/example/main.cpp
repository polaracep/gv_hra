#include <SDL/SDL.h>

#include "../grafika.h"

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
