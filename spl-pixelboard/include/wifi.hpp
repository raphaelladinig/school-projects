#ifndef WIFI_HPP
#define WIFI_HPP

class WiFiManager {
  public:
    WiFiManager(const char *ssid, const char *password);
    void begin();
    bool isConnected();
    void reconnect();

  private:
    const char *ssid;
    const char *password;
};

#endif
