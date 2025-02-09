OS := $(shell uname -s)

SOURCES = $(wildcard *.cpp)
OBJECTS = $(SOURCES:.cpp=.o)
DEPENDS = $(SOURCES:.cpp=.d)
TARGET = program

CFLAGS = -ggdb

# OS-Specific Flags
ifeq ($(OS), Linux)
    LFLAGS = -lSDL -lSDL_image
else
    # Assuming Windows (MinGW)
    CFLAGS += -I C:\Qt\SDL-1.2.15\include \
              -I C:\Qt\SDL-1.2.15\include\SDL \
              -I C:\Qt\SDL_image-1.2.12\include

    LFLAGS = -L C:\Qt\SDL-1.2.15\lib \
             -L C:\Qt\SDL_image-1.2.12\lib\x86 \
             -lmingw32 -lSDLmain -lSDL -lSDL_image
endif


all: $(TARGET)

$(TARGET): $(OBJECTS)
	g++ $(OBJECTS) -o $(TARGET) $(LFLAGS)

%.o: %.cpp
	g++ -MMD -MP $(CFLAGS) -c $< -o $@

-include $(DEPENDS)
