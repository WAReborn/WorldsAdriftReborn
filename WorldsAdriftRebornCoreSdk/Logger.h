#pragma once
#include <iostream>
#include <fstream>
#include <iomanip>

class Logger
{
    static Logger* logger;

    std::ofstream output;

    Logger(Logger& other) = delete;
    void operator=(const Logger&) = delete;

    Logger() {
        output.open("CoreSdk_OutputLog.txt", std::ios::app);
    }

    ~Logger() {
        output.close();
    }

public:
    static Logger* GetLogger() {
        if (!logger) {
            logger = new Logger();
        }
        return logger;
    }

    void debug(std::string msg) {
        std::cerr << msg << std::endl;
        output << msg << std::endl;
    }

    static void toHex(char c) {
        std::cerr << std::hex << std::setw(2) << std::setfill('0') << (int)static_cast<unsigned char>(c) << " ";
    }

    static void Debug(std::string msg) {
        GetLogger()->debug(msg);
    }

    static void Hexify(char* buffer, int length) {
        for (int i = 0; i < length; i++) {
            toHex(buffer[i]);
        }
        std::cerr << std::endl;
    }
};

