#ifndef GRAFIKA_H
#define GRAFIKA_H

#include <SDL/SDL.h>

//
// Funkce pro carove kresleni
//

#define WHITE 255, 255, 255
#define RED 255, 0, 0
#define GREEN 0, 255, 0
#define BLUE 0, 0, 255
#define CYAN 0, 255, 255
#define PURPLE 255, 0, 255
#define BLACK 0, 0, 0
#define YELLOW 255, 255, 0
float random(float max);

//
// Zapouzdreni obrazovky
//

class Screen {
public:
    SDL_Surface* screen;
    unsigned int w, h;
    float x, y;

    static Screen* instance();
    void init(const unsigned int width, const unsigned int height, const unsigned int barvy, const unsigned int vlajky);
    void place(float x, float y);
    void move(float x, float y);
    void update();
    Uint32 get_pixel(int x, int y);
    void put_pixel(int x, int y, Uint32 pixel);
    void lock();
    void unlock();
    void remove();

protected:
    Screen();
};

//
// Graficky objekt na obrazovce
//

class Image {
public:
    // Vnitrni promenne objektu
    SDL_Surface* surface;
    Screen* screen;
    float x, y, vx, vy, ax, ay, ox, oy;
    int w, h;

    // Konstruktor
    Image();

    // Destruktor
    ~Image();

    // Nacteni obrazku z disku do pameti
    void load(const char* filename);
    void load_animation(char* filename, int w, int h);

    // Nedovol objektu opustit obrazovku
    void bounds();

    void origin(float px, float py);

    // Presun objektu na zadane souradnice
    void place(float px, float py);

    // Pohyb objektu o zadanou vzdalenost
    void move(float px, float py);

    // Nastav velocity automatickeho pohybu
    void velocity(float x, float y);

    void accel(float x, float y);

    // Aktualizuj automaticke pohyby
    void update();

    // Vykresleni objektu na obrazovku
    void draw();

    // Zjistime kolizi
    int collision(Image* o);
    Uint32 get_pixel(int x, int y);
};

class Animation : public Image {
public:
    int frames, frame;

    Animation();

    // Nacteni obrazku z disku do pameti
    void load(char* filename, int w, int h);

    // Aktualizuj automaticke pohyby
    void update();

    // Vykresleni objektu na obrazovku
    void draw();
};

class Text : public Image {
public:
    char characters[256];
    int positions[256];
    int width[256];
    int count;

    Text();

    void load(const char* filename, const char* characters);
    void draw(const char* text);
    void draw(int cislo);
};

extern Uint32 pixel;

void color(unsigned char r, unsigned char g, unsigned char b);
void point(int x, int y);
void line(int x1, int y1, int x2, int y2);
void line(int x, int y);
void rline(int x, int y);
void rectangle(int x1, int y1, int x2, int y2);

#endif // MAIN_H
