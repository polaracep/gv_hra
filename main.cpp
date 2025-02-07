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
#include <stdio.h>
#include <SDL/SDL.h>
    enum {
        DISPLAY_WIDTH  = 480
        , DISPLAY_HEIGHT = 320
        , UPDATE_INTERVAL = 1000/60
        , HERO_SPEED = 2
    };

    class Sprite {
    public:
        int x, y ;
        Sprite() :x(0), y(0) {}
    } ;

    class Game {
    public:
        Game();
        ~Game();
        void start();
        void stop() ;
        void draw();
        void fillRect( SDL_Surface* surface, SDL_Rect* rc, int r, int g, int b );
        void fpsChanged( int fps );
        void onQuit();
        void onKeyDown( SDL_Event* event );
        void onKeyUp( SDL_Event* event );
        void run();
        void update();
    private:
        int keys[ SDLK_LAST ] ;
        int frameSkip ;
        int running ;
        SDL_Surface* display ;
        Sprite hero ;
    };

    Game::Game()
        :frameSkip(0), running(0), display(NULL) {
        for ( int i=0; i<SDLK_LAST; ++i ) {
            this->keys[ i ] = 0 ;
        }
    }

    Game::~Game() {
        this->stop();
    }

    void Game::start() {
        int flags = SDL_HWSURFACE|SDL_DOUBLEBUF|SDL_ANYFORMAT ;

        /* initialize SDL */
        if ( SDL_Init(SDL_INIT_VIDEO|SDL_INIT_TIMER) ) {
            return ;
        }

        /* set video surface */
        this->display = SDL_SetVideoMode( DISPLAY_WIDTH, DISPLAY_HEIGHT, 0, flags );
        if ( display == NULL ) {
            return ;
        }
        /* Set caption */
        SDL_WM_SetCaption( "SDL Base C++", NULL );

        this->running = 1 ;
        run();
    }

    void Game::stop() {
        if ( display != NULL ) {
            SDL_FreeSurface( display );
        }
        SDL_Quit() ;
    }

    void Game::draw() {
        SDL_Rect heroRect ;

        /* Clear screen */
        fillRect( display, NULL, 255, 255, 255 );

        /* Render hero */
        heroRect.x = hero.x ;
        heroRect.y = hero.y ;
        heroRect.w = 20 ;
        heroRect.h = 20 ;
        fillRect( display, &heroRect, 255, 0, 0 );

        SDL_Flip( display );
    }

    void Game::fillRect( SDL_Surface* surface, SDL_Rect* rc, int r, int g, int b ) {
        SDL_FillRect( surface, rc, SDL_MapRGB(surface->format, r, g, b) );
    }

    void Game::fpsChanged( int fps ) {
        char szFps[ 128 ] ;
        sprintf( szFps, "%s: %d FPS", "SDL Base C++ - Use Arrow Keys to Move", fps );
        SDL_WM_SetCaption( szFps, NULL );
    }

    void Game::onQuit() {
        running = 0 ;
    }

    void Game::run() {
        int past = SDL_GetTicks();
        int now = past, pastFps = past ;
        int fps = 0, framesSkipped = 0 ;
        SDL_Event event ;
        while ( running ) {
            int timeElapsed = 0 ;
            if (SDL_PollEvent(&event)) {
                switch (event.type) {
                case SDL_QUIT:    onQuit();            break;
                case SDL_KEYDOWN: onKeyDown( &event ); break ;
                case SDL_KEYUP:   onKeyUp( &event );   break ;
                case SDL_MOUSEBUTTONDOWN: {
                    break ;
                }
                case SDL_MOUSEBUTTONUP: {
                    break ;
                }
                case SDL_MOUSEMOTION: {
                    break ;
                }
                }
            }
            /* update/draw */
            timeElapsed = (now=SDL_GetTicks()) - past ;
            if ( timeElapsed >= UPDATE_INTERVAL  ) {
                past = now ;
                update();
                if ( framesSkipped++ >= frameSkip ) {
                    draw();
                    ++fps ;
                    framesSkipped = 0 ;
                }
            }
            /* fps */
            if ( now - pastFps >= 1000 ) {
                pastFps = now ;
                fpsChanged( fps );
                fps = 0 ;
            }
            /* sleep? */
            SDL_Delay( 1 );
        }
    }

    void Game::update() {
        if ( keys[SDLK_LEFT] ) {
            hero.x -= HERO_SPEED ;
        } else if ( keys[SDLK_RIGHT] ) {
            hero.x += HERO_SPEED ;
        } else if ( keys[SDLK_UP] ) {
            hero.y -= HERO_SPEED ;
        } else if ( keys[SDLK_DOWN] ) {
            hero.y += HERO_SPEED ;
        }
    }

    void Game::onKeyDown( SDL_Event* evt ) {
        keys[ evt->key.keysym.sym ] = 1 ;
    }

    void Game::onKeyUp( SDL_Event* evt ) {
        keys[ evt->key.keysym.sym ] = 0 ;
    }

    int main(int argc, char *argv[]) {
        Game game ;
        game.start();
        return 0 ;
    }
