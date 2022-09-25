#pragma once
#include <iostream>
#include <fstream>
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

    static void Debug(std::string msg) {
        GetLogger()->debug(msg);
    }
};

