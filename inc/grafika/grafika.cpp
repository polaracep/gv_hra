#include <math.h>
#include <SDL/SDL.h>

#ifdef _WIN32
    #include <SDL_image.h>
#else
    #include <SDL/SDL_image.h>
#endif

#include "grafika.h"

using namespace std;

float nahoda(float max)
   {
       return (float)rand() / RAND_MAX * max;
   }

//
// Zapouzreni obrazovky
//

Obrazovka::Obrazovka()
{
	x = 0;
	y = 0;
}

Obrazovka* Obrazovka::instance()
{
	static Obrazovka inst;
	return &inst;
}

void Obrazovka::inicializuj(const unsigned int sirka, const unsigned int vyska, const unsigned int barvy, const unsigned int vlajky)
{
	w = sirka;
	h = vyska;

	// Inicializuji grafickou knihovnu
	if(SDL_Init(SDL_INIT_VIDEO | SDL_INIT_AUDIO))
	{
		printf("SDL_Init() selhalo: %s\n", SDL_GetError());
		exit(1);
	}

	// Otevru okno na obrazovce
	if(!(screen = SDL_SetVideoMode(sirka, vyska, barvy, vlajky)))
	{
		printf("SDL_SetVideoMode() selhalo: %s\n", SDL_GetError());
		SDL_Quit();
		exit(1);
	}
}

void Obrazovka::umisti(float x, float y)
{
	this->x = x;
	this->y = y;
}

void Obrazovka::pohni(float x, float y)
{
	this->x += x;
	this->y += y;
}

void Obrazovka::aktualizuj()
{
	SDL_UpdateRect(screen, 0, 0, 0, 0);
}

void Obrazovka::smaz()
{
	SDL_FillRect(screen, NULL, 0);
}

Uint32 Obrazovka::getpixel(int x, int y)
{
	if(x < 0 || x >= w || y < 0 || y >= h) return 0;

    int bpp = screen->format->BytesPerPixel;
    /* Here p is the address to the pixel we want to retrieve */
    Uint8 *p = (Uint8 *)screen->pixels + y * screen->pitch + x * bpp;

    switch(bpp) {
    case 1:
	return *p;

    case 2:
	return *(Uint16 *)p;

    case 3:
	if(SDL_BYTEORDER == SDL_BIG_ENDIAN)
	    return p[0] << 16 | p[1] << 8 | p[2];
	else
	    return p[0] | p[1] << 8 | p[2] << 16;

    case 4:
	return *(Uint32 *)p;

    default:
	return 0;       /* shouldn't happen, but avoids warnings */
    }
}

void Obrazovka::putpixel(int x, int y, Uint32 pixel)
{
	if(x < 0 || x >= w || y < 0 || y >= h) return;

	Uint8 *p = (Uint8 *)screen->pixels + y * screen->pitch + x * screen->format->BytesPerPixel;

	switch(screen->format->BytesPerPixel)
	{
	case 1:
		*p = pixel;
		break;
	case 2:
		*(Uint16 *)p = pixel;
		break;
	case 3:
		if(SDL_BYTEORDER == SDL_BIG_ENDIAN)
		{
			p[0] = (pixel >> 16) & 0xff;
			p[1] = (pixel >> 8) & 0xff;
			p[2] = pixel & 0xff;
		}
		else
		{
			p[0] = pixel & 0xff;
			p[1] = (pixel >> 8) & 0xff;
			p[2] = (pixel >> 16) & 0xff;
		}
		break;
	case 4:
		*(Uint32 *)p = pixel;
		break;
	}
}

void Obrazovka::zamkni()
{
	if((SDL_MUSTLOCK(screen)) && (SDL_LockSurface(screen) < 0))
	{
		printf("Can't lock screen: %s\n", SDL_GetError());
		return;
	}
}

void Obrazovka::odemkni()
{
	if(SDL_MUSTLOCK(screen)) SDL_UnlockSurface(screen);
}

//
// Graficky objekt na obrazovce
//

Obrazek::Obrazek()
{
	this->surface = NULL;
	x = y = vx = vy = ax = ay = ox = oy = 0;
	w = h = 0;
	obrazovka = Obrazovka::instance();
}

// Destruktor
Obrazek::~Obrazek()
{
	SDL_FreeSurface(surface);
}

// Nacteni obrazku z disku do pameti
void Obrazek::nacti(const char *filename)
{
	SDL_Surface *tmp;
	if(!(tmp = IMG_Load(filename)))
	{
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
void Obrazek::meze()
{
	if(this->x < 0) this->x = 0;
	if(this->y < 0) this->y = 0;
	if(this->x > obrazovka->w - this->w) this->x = obrazovka->w - this->w;
	if(this->y > obrazovka->h - this->h) this->y = obrazovka->h - this->h;
}

// Presun pocatek objektu
void Obrazek::pocatek(float px, float py)
{
	ox = px;
	oy = py;
}

// Presun objektu na zadane souradnice
void Obrazek::umisti(float px, float py)
{
	x = px;
	y = py;
}

// Pohyb objektu o zadanou vzdalenost
void Obrazek::pohni(float px, float py)
{
	x += px;
	y += py;
}

// Nastav rychlost automatickeho pohybu
void Obrazek::rychlost(float x, float y)
{
	vx = x;
	vy = y;
}

void Obrazek::zrychleni(float x, float y)
{
	ax = x;
	ay = y;
}

// Aktualizuj automaticke pohyby
void Obrazek::aktualizuj()
{
	vx += ax;
	vy += ay;
	x += vx;
	y += vy;
	//printf("%f,%f %f,%f %f,%f\n", x, y, vx, vy, ax, ay);
}

// Vykresleni objektu na obrazovku
void Obrazek::kresli()
{
	SDL_Rect drect;

	drect.x = (int)round(x - ox - obrazovka->x);
	drect.y = (int)round(y - oy - obrazovka->y);
	SDL_BlitSurface(this->surface, NULL, obrazovka->screen, &drect);

	/*cara(drect.x, drect.y, drect.x + w, drect.y + h);
	cara(drect.x, drect.y + h, drect.x + w, drect.y);*/
}

// Zjistime kolizi
int Obrazek::kolize(Obrazek *o)
{
	if
	(
		(o->x - o->ox + o->w) >= this->x - this->ox
		&& o->x - o->ox <= (this->x - this->ox + this->w)
		&& (o->y - o->oy + o->h) >= this->y - this->oy
		&& o->y - o->oy <= (this->y -this->oy + this->h)
	) return 1;
	else return 0;
}

Uint32 Obrazek::getpixel(int x,int y)
{
int bpp = this->surface->format->BytesPerPixel;
    /* Here p is the address to the pixel we want to retrieve */
    Uint8 *p = (Uint8 *)this->surface->pixels + y * this->surface->pitch + x * bpp;

    switch(bpp) {
    case 1:
	return *p;

    case 2:
	return *(Uint16 *)p;

    case 3:
	if(SDL_BYTEORDER == SDL_BIG_ENDIAN)
	    return p[0] << 16 | p[1] << 8 | p[2];
	else
	    return p[0] | p[1] << 8 | p[2] << 16;

    case 4:
	return *(Uint32 *)p;

    default:
	return 0;       /* shouldn't happen, but avoids warnings */
    }
   }
Animace::Animace()
{
	frames = frame = 0;
}

void Animace::nacti(char *filename, int w, int h)
{
	SDL_Surface *tmp;

	if(!(tmp = IMG_Load(filename)))
	{
		printf("Chyba: Nepodarilo se nahrat obrazek ze souboru: '%s'", filename);
		SDL_Quit();
		exit(1);
	}

	this->surface = SDL_DisplayFormatAlpha(tmp);
	SDL_FreeSurface(tmp);

	this->w = w;
	this->h = h;

	this->frames = this->surface->w / w;
}

void Animace::aktualizuj()
{
	Obrazek::aktualizuj();

	if(this->frames)
	{
		this->frame++;
		if(this->frame == this->frames) this->frame = 0;
	}
}

void Animace::kresli()
{
	SDL_Rect srect, drect;

	srect.x = this->frame * this->w;
	srect.y = 0;
	srect.w = this->w;
	srect.h = this->h;
	drect.x = (int)round(x - ox - obrazovka->x);
	drect.y = (int)round(y - oy - obrazovka->y);
	SDL_BlitSurface(this->surface, &srect, obrazovka->screen, &drect);
}

Pismo::Pismo()
{
	this->pocet = 0;
}

void Pismo::nacti(const char *filename, const char *characters)
{
	unsigned int i, j, prazdno, hledam;

	Obrazek::nacti(filename);
	strcpy(this->znaky, characters);

	hledam = 1;
	for(i = 0; i < this->w; i++)
	{
		prazdno = 1;
		for(j = 0; j < this->h; j++)
		{
			if((this->getpixel(i, j) & 0xff000000) != 0)
			{
				prazdno = 0;
				break;
			}
		}

		if(hledam == 1 && prazdno == 0)
		{
			this->pozice[this->pocet] = i;
			hledam = 0;
		}
		if(hledam == 0 && prazdno == 1)
		{
			this->sirka[this->pocet] = i - this->pozice[this->pocet];
			hledam = 1;
			this->pocet++;
		}
	}
	for(i = 0; i < this->pocet; i++) printf("%d: %d %d\n", i, this->pozice[i], this->sirka[i]);
}
void Pismo::kresli(const char *text)
{
	SDL_Rect srect, drect;
	int i, x, j;

	x = (int)round(this->x);

	for(i = 0; i < strlen(text); i++)
	{
		for(j = 0; j < this->pocet; j++)
		{
			if(this->znaky[j] == text[i])
			{
				srect.x = this->pozice[j];
				srect.y = 0;
				srect.w = this->sirka[j];
				srect.h = this->h;
				drect.x = x;
				drect.y = (int)this->y;
				SDL_BlitSurface(this->surface, &srect, obrazovka->screen, &drect);
				x += this->sirka[j];
			}
		}
	}
}

void Pismo::kresli(int cislo)
{
	char text[256];

	sprintf(text, "%d", cislo);
	this->kresli(text);
}

// Jednoduche globalni kreslici funkce

Uint32 pixel = 0;
int lx = 0, ly = 0;

#define BILA 255, 255, 255
#define CERVENA 255, 0, 0
#define ZELENA 0, 255, 0
#define MODRA 0, 0, 255
#define AZUROVA 0, 255, 255
#define PURPUROVA 255, 0, 255
#define ZLUTA 255, 255, 0

void barva(unsigned char r, unsigned char g, unsigned char b)
{
	Obrazovka* obrazovka = Obrazovka::instance();
	pixel = SDL_MapRGB(obrazovka->screen->format, r, g, b);
}

void bod(int x, int y)
{
	Obrazovka* obrazovka = Obrazovka::instance();

	obrazovka->zamkni();
	obrazovka->putpixel(x, y, pixel);
	obrazovka->odemkni();

	lx = x; ly = y;
}

void cara(int x1, int y1, int x2, int y2)
{
	int i, a;
	float k, p;

	Obrazovka* obrazovka = Obrazovka::instance();

	lx = x2; ly = y2;

	obrazovka->zamkni();

	if(abs(x2 - x1) >= abs(y2 - y1))
	{
		if(x2 < x1)
		{
			a = y1; y1 = y2; y2 = a;
			a = x1; x1 = x2; x2 = a;
		}

		k = (float)(y2 - y1) / (x2 - x1);
		for(i = x1, p = y1; i <= x2; i++, p += k)
		{
			obrazovka->putpixel(i, (int)p, pixel);
		}
	}
	else
	{
		if(y2 < y1)
		{
			a = y1; y1 = y2; y2 = a;
			a = x1; x1 = x2; x2 = a;
		}

		k = (float)(x2 - x1) / (y2 - y1);
		for(i = y1, p = x1; i <= y2; i++, p += k)
		{
			obrazovka->putpixel((int)p, i, pixel);
		}
	}
	obrazovka->odemkni();
}

void cara(int x, int y)
{
	cara(lx, ly, x, y);
}

void rcara(int x, int y)
{
	cara(lx, ly, lx + x, ly + y);
}

void obdelnik(int x1, int y1, int x2, int y2)
{
	Obrazovka* obrazovka = Obrazovka::instance();

	SDL_Rect rect;
	rect.x = x1;
	rect.y = y1;
	rect.w = x2 - x1 + 1;
	rect.h = y2 - y1 + 1;
	SDL_FillRect(obrazovka->screen, &rect, pixel);
}
