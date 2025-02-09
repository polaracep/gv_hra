#include <math.h>
#include <SDL/SDL.h>

#ifdef _WIN32
#include <SDL_image.h>
#else
#include <SDL/SDL_image.h>
#endif

#include "grafika.h"

using namespace std;

float random(float max)
{
    return (float)rand() / RAND_MAX * max;
}

//
// Zapouzreni obrazovky
//

Screen* Screen::instance()
{
    static Screen inst;
    return &inst;
}

void Screen::init(const unsigned int width, const unsigned int height, const unsigned int barvy, const unsigned int vlajky)
{
    w = width;
    h = height;

    // Inicializuji grafickou knihovnu
    if (SDL_Init(SDL_INIT_VIDEO | SDL_INIT_AUDIO)) {
        printf("SDL_Init() err: %s\n", SDL_GetError());
        exit(1);
    }

    // Otevru okno na obrazovce
    if (!(screen = SDL_SetVideoMode(width, height, barvy, vlajky))) {
        printf("SDL_SetVideoMode() err: %s\n", SDL_GetError());
        SDL_Quit();
        exit(1);
    }
}

void Screen::place(float x, float y)
{
    this->x = x;
    this->y = y;
}

void Screen::move(float x, float y)
{
    this->x += x;
    this->y += y;
}

void Screen::update()
{
    SDL_UpdateRect(screen, 0, 0, 0, 0);
}

void Screen::remove()
{
    SDL_FillRect(screen, NULL, 0);
}

Uint32 Screen::get_pixel(int x, int y)
{
    if (x < 0 || x >= w || y < 0 || y >= h)
        return 0;

    int bpp = screen->format->BytesPerPixel;
    /* Here p is the address to the pixel we want to retrieve */
    Uint8* p = (Uint8*)screen->pixels + y * screen->pitch + x * bpp;

    switch (bpp) {
    case 1:
        return *p;

    case 2:
        return *(Uint16*)p;

    case 3:
        if (SDL_BYTEORDER == SDL_BIG_ENDIAN)
            return p[0] << 16 | p[1] << 8 | p[2];
        else
            return p[0] | p[1] << 8 | p[2] << 16;

    case 4:
        return *(Uint32*)p;

    default:
        return 0; /* shouldn't happen, but avoids warnings */
    }
}

void Screen::put_pixel(int x, int y, Uint32 pixel)
{
    if (x < 0 || x >= w || y < 0 || y >= h)
        return;

    Uint8* p = (Uint8*)screen->pixels + y * screen->pitch + x * screen->format->BytesPerPixel;

    switch (screen->format->BytesPerPixel) {
    case 1:
        *p = pixel;
        break;
    case 2:
        *(Uint16*)p = pixel;
        break;
    case 3:
        if (SDL_BYTEORDER == SDL_BIG_ENDIAN) {
            p[0] = (pixel >> 16) & 0xff;
            p[1] = (pixel >> 8) & 0xff;
            p[2] = pixel & 0xff;
        } else {
            p[0] = pixel & 0xff;
            p[1] = (pixel >> 8) & 0xff;
            p[2] = (pixel >> 16) & 0xff;
        }
        break;
    case 4:
        *(Uint32*)p = pixel;
        break;
    }
}

void Screen::lock()
{
    if ((SDL_MUSTLOCK(screen)) && (SDL_LockSurface(screen) < 0)) {
        printf("Can't lock screen: %s\n", SDL_GetError());
        return;
    }
}

void Screen::unlock()
{
    if (SDL_MUSTLOCK(screen))
        SDL_UnlockSurface(screen);
}

//
// Graficky objekt na obrazovce
//

Image::Image()
{
    this->surface = NULL;
    x = y = vx = vy = ax = ay = ox = oy = 0;
    w = h = 0;
    screen = Screen::instance();
}

// Destruktor
Image::~Image()
{
    SDL_FreeSurface(surface);
}

// Nacteni obrazku z disku do pameti
void Image::load(const char* filename)
{
    SDL_Surface* tmp;
    if (!(tmp = IMG_Load(filename))) {
        printf("Chyba: Nepodarilo se nahrat obrazek ze souboru: '%s'", filename);
        SDL_Quit();
        exit(1);
    }
    this->surface = SDL_DisplayFormatAlpha(tmp);
    SDL_FreeSurface(tmp);
    this->w = this->surface->w;
    this->h = this->surface->h;
}

// Nedovol objektu opustit obrazovku
void Image::bounds()
{
    if (this->x < 0)
        this->x = 0;
    if (this->y < 0)
        this->y = 0;
    if (this->x > screen->w - this->w)
        this->x = screen->w - this->w;
    if (this->y > screen->h - this->h)
        this->y = screen->h - this->h;
}

// Presun origin objektu
void Image::origin(float px, float py)
{
    ox = px;
    oy = py;
}

// Presun objektu na zadane souradnice
void Image::place(float px, float py)
{
    x = px;
    y = py;
}

// Pohyb objektu o zadanou vzdalenost
void Image::move(float px, float py)
{
    x += px;
    y += py;
}

// Nastav velocity automatickeho pohybu
void Image::velocity(float x, float y)
{
    vx = x;
    vy = y;
}

void Image::accel(float x, float y)
{
    ax = x;
    ay = y;
}

// Aktualizuj automaticke pohyby
void Image::update()
{
    vx += ax;
    vy += ay;
    x += vx;
    y += vy;
    // printf("%f,%f %f,%f %f,%f\n", x, y, vx, vy, ax, ay);
}

// Vykresleni objektu na obrazovku
void Image::draw()
{
    SDL_Rect drect;

    drect.x = (int)round(x - ox - screen->x);
    drect.y = (int)round(y - oy - screen->y);
    SDL_BlitSurface(this->surface, NULL, screen->screen, &drect);

    /*cara(drect.x, drect.y, drect.x + w, drect.y + h);
        cara(drect.x, drect.y + h, drect.x + w, drect.y);*/
}

// Zjistime kolizi
int Image::collision(Image* o)
{
    if (
        (o->x - o->ox + o->w) >= this->x - this->ox
        && o->x - o->ox <= (this->x - this->ox + this->w)
        && (o->y - o->oy + o->h) >= this->y - this->oy
        && o->y - o->oy <= (this->y - this->oy + this->h))
        return 1;
    else
        return 0;
}

Uint32 Image::get_pixel(int x, int y)
{
    int bpp = this->surface->format->BytesPerPixel;
    /* Here p is the address to the pixel we want to retrieve */
    Uint8 *p = (Uint8 *)this->surface->pixels + y * this->surface->pitch + x * bpp;

    switch (bpp) {
    case 1:
        return *p;

    case 2:
        return *(Uint16*)p;

    case 3:
        if (SDL_BYTEORDER == SDL_BIG_ENDIAN)
            return p[0] << 16 | p[1] << 8 | p[2];
        else
            return p[0] | p[1] << 8 | p[2] << 16;

    case 4:
        return *(Uint32*)p;

    default:
        return 0; /* shouldn't happen, but avoids warnings */
    }
}
Animation::Animation()
{
    frames = frame = 0;
}

void Animation::load(char* filename, int w, int h)
{
    SDL_Surface* tmp;

    if (!(tmp = IMG_Load(filename))) {
        printf("Couldn't load image from file: '%s'", filename);
        SDL_Quit();
        exit(1);
    }

    this->surface = SDL_DisplayFormatAlpha(tmp);
    SDL_FreeSurface(tmp);

    this->w = w;
    this->h = h;

    this->frames = this->surface->w / w;
}

void Animation::update()
{
    Image::update();

    if (this->frames) {
        this->frame++;
        if (this->frame == this->frames)
            this->frame = 0;
    }
}

void Animation::draw()
{
    SDL_Rect srect, drect;

    srect.x = this->frame * this->w;
    srect.y = 0;
    srect.w = this->w;
    srect.h = this->h;
    drect.x = (int)round(x - ox - screen->x);
    drect.y = (int)round(y - oy - screen->y);
    SDL_BlitSurface(this->surface, &srect, screen->screen, &drect);
}

Text::Text()
{
    this->count = 0;
}

void Text::load(const char* filename, const char* characters)
{
    unsigned int i, j, empty, searching;

    Image::load(filename);
    strcpy(this->characters, characters);

    searching = 1;
    for (i = 0; i < this->w; i++) {
        empty = 1;
        for (j = 0; j < this->h; j++) {
            if ((this->get_pixel(i, j) & 0xff000000) != 0) {
                empty = 0;
                break;
            }
        }

        if (searching == 1 && empty == 0) {
            this->positions[this->count] = i;
            searching = 0;
        }
        if (searching == 0 && empty == 1) {
            this->width[this->count] = i - this->positions[this->count];
            searching = 1;
            this->count++;
        }
    }
    for (i = 0; i < this->count; i++)
        printf("%d: %d %d\n", i, this->positions[i], this->width[i]);
}
void Text::draw(const char* text)
{
    SDL_Rect srect, drect;
    int i, x, j;

    x = (int)round(this->x);

    for (i = 0; i < strlen(text); i++) {
        for (j = 0; j < this->count; j++) {
            if (this->characters[j] == text[i]) {
                srect.x = this->positions[j];
                srect.y = 0;
                srect.w = this->width[j];
                srect.h = this->h;
                drect.x = x;
                drect.y = (int)this->y;
                SDL_BlitSurface(this->surface, &srect, screen->screen, &drect);
                x += this->width[j];
            }
        }
    }
}

void Text::draw(int cislo)
{
    char text[256];

    sprintf(text, "%d", cislo);
    this->draw(text);
}

// Jednoduche globalni kreslici funkce

Uint32 pixel = 0;
int lx = 0, ly = 0;

#define WHITE 255, 255, 255
#define RED 255, 0, 0
#define GREEN 0, 255, 0
#define BLUE 0, 0, 255
#define CYAN 0, 255, 255
#define PURPLE 255, 0, 255
#define YELLOW 255, 255, 0

void color(unsigned char r, unsigned char g, unsigned char b)
{
    Screen* screen = Screen::instance();
    pixel = SDL_MapRGB(screen->screen->format, r, g, b);
}

void point(int x, int y)
{
    Screen* screen = Screen::instance();

    screen->lock();
    screen->put_pixel(x, y, pixel);
    screen->unlock();

    lx = x;
    ly = y;
}

void line(int x1, int y1, int x2, int y2)
{
    int i, a;
    float k, p;

    Screen* screen = Screen::instance();

    lx = x2;
    ly = y2;

    screen->lock();

    if (abs(x2 - x1) >= abs(y2 - y1)) {
        if (x2 < x1) {
            a = y1;
            y1 = y2;
            y2 = a;
            a = x1;
            x1 = x2;
            x2 = a;
        }

        k = (float)(y2 - y1) / (x2 - x1);
        for (i = x1 p = y1; i <= x2; i++, p += k) {
            screen->put_pixel(i, (int)p, pixel);
        }
    } else {
        if (y2 < y1) {
            a = y1;
            y1 = y2;
            y2 = a;
            a = x1;
            x1 = x2;
            x2 = a;
        }

        k = (float)(x2 - x1) / (y2 - y1);
        for (i = y1, p = x1; i <= y2; i++, p += k) {
            screen->put_pixel((int)p, i, pixel);
        }
    }
    screen->unlock();
}

void line(int x, int y)
{
    line(lx, ly, x, y);
}

void rline(int x, int y)
{
    line(lx, ly, lx + x, ly + y);
}

void rectangle(int x1, int y1, int x2, int y2)
{
    Screen* screen = Screen::instance();

    SDL_Rect rect;
    rect.x = x1;
    rect.y = y1;
    rect.w = x2 - x1 + 1;
    rect.h = y2 - y1 + 1;
    SDL_FillRect(screen->screen, &rect, pixel);
}
