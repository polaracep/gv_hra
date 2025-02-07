#ifndef OBRAZEK_H
#define OBRAZEK_H

#include <SDL/SDL.h>

//
// Funkce pro carove kresleni
//

#define BILA 255, 255, 255
#define CERVENA 255, 0, 0
#define ZELENA 0, 255, 0
#define MODRA 0, 0, 255
#define AZUROVA 0, 255, 255
#define PURPUROVA 255, 0, 255
#define CERNA 0, 0, 0
#define ZLUTA 255, 255, 0
float nahoda(float max);

//
// Zapouzdreni obrazovky
//

class Obrazovka
{
public:
	SDL_Surface *screen;
	unsigned int w, h;
	float x, y;
	
	static Obrazovka* instance();
	void inicializuj(const unsigned int sirka, const unsigned int vyska, const unsigned int barvy, const unsigned int vlajky);
	void umisti(float x, float y);
	void pohni(float x, float y);
	void aktualizuj();
	Uint32 getpixel(int x, int y);
	void putpixel(int x, int y, Uint32 pixel);
	void zamkni();
	void odemkni();
	void smaz();
protected:
	Obrazovka();
};


//
// Graficky objekt na obrazovce
//

class Obrazek
{
public:
	// Vnitrni promenne objektu
	SDL_Surface *surface;
	Obrazovka *obrazovka;
	float x, y, vx, vy, ax, ay, ox, oy;
	int w, h;
	
	// Konstruktor
	Obrazek();

	// Destruktor
	~Obrazek();

	// Nacteni obrazku z disku do pameti
	void nacti(const char *filename);
	void nacti_animaci(char *filename, int w, int h);

	// Nedovol objektu opustit obrazovku
	void meze();
	
	void pocatek(float px, float py);

	// Presun objektu na zadane souradnice
	void umisti(float px, float py);

	// Pohyb objektu o zadanou vzdalenost
	void pohni(float px, float py);

	// Nastav rychlost automatickeho pohybu
	void rychlost(float x, float y);

	void zrychleni(float x, float y);

	// Aktualizuj automaticke pohyby
	void aktualizuj();

	// Vykresleni objektu na obrazovku
	void kresli();

	// Zjistime kolizi
	int kolize(Obrazek *o);
	Uint32 getpixel(int x,int y);
};

class Animace: public Obrazek
{
public:
	int frames, frame;
	
	Animace();

	// Nacteni obrazku z disku do pameti
	void nacti(char *filename, int w, int h);

	// Aktualizuj automaticke pohyby
	void aktualizuj();

	// Vykresleni objektu na obrazovku
	void kresli();
};

class Pismo: public Obrazek
{
public:
	char znaky[256];
	int pozice[256];
	int sirka[256];
	int pocet;
	
	Pismo();
	
	void nacti(const char *filename, const char *characters);
	void kresli(const char *text);
	void kresli(int cislo);
};

extern Uint32 pixel;

void barva(unsigned char r, unsigned char g, unsigned char b);
void bod(int x, int y);
void cara(int x1, int y1, int x2, int y2);
void cara(int x, int y);
void rcara(int x, int y);
void obdelnik(int x1, int y1, int x2, int y2);

#endif // MAIN_H
