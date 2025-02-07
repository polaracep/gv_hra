cmake_minimum_required(VERSION 3.16)

set(TARGET_NAME gv_hra)
project(${TARGET_NAME} LANGUAGES CXX)

set(CMAKE_CXX_STANDARD 17)
set(CMAKE_CXX_STANDARD_REQUIRED ON)

set(PROJECT_SOURCES main.cpp inc/grafika/grafika.cpp)
set(PROJECT_INCLUDES inc/grafika/grafika.h)

add_executable(${TARGET_NAME}
${PROJECT_SOURCES}
${PROJECT_INCLUDES})

# SDL
find_package(SDL REQUIRED)
find_package(SDL_image REQUIRED)
include_directories(${SDL_INCLUDE_DIR})
include_directories(${SDLIMAGE_INCLUDE_DIR})
target_link_libraries(${TARGET_NAME} PUBLIC ${SDL_LIBRARY})
target_link_libraries(${TARGET_NAME} PUBLIC ${SDLIMAGE_LIBRARY})

# Grafika lib
include_directories(${CMAKE_SOURCE_DIR}/inc)


include(GNUInstallDirs)
install(TARGETS ${TARGET_NAME}
    LIBRARY DESTINATION ${CMAKE_INSTALL_LIBDIR}
    RUNTIME DESTINATION ${CMAKE_INSTALL_BINDIR}
)
