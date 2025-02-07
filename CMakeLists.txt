cmake_minimum_required(VERSION 3.16)

set(TARGET_NAME gv_hra)
project(${TARGET_NAME} LANGUAGES CXX)

set(CMAKE_CXX_STANDARD 17)
set(CMAKE_CXX_STANDARD_REQUIRED ON)

add_executable(${TARGET_NAME} main.cpp)

# SDL
find_package(SDL REQUIRED)
include_directories(${SDL_INCLUDE_DIR})
target_link_libraries(${TARGET_NAME} PRIVATE ${SDL_LIBRARY})

# Grafika lib
include_directories(${CMAKE_SOURCE_DIR}/inc)

include(GNUInstallDirs)
install(TARGETS ${TARGET_NAME}
    LIBRARY DESTINATION ${CMAKE_INSTALL_LIBDIR}
    RUNTIME DESTINATION ${CMAKE_INSTALL_BINDIR}
)
