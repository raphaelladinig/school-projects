- [ ] db connection fixn

    1) Formulare & Formularüberprüfung
        a) html: Registrierungsformular erzeugen
        b) css: mit Bootstrap gestalten
        c) Controllermethode programmieren
            i) Library erzeugen (Validierungsklasse)
            ii) Eingabedaten validieren
                (1) Nachname (mind. 2 Zeichen lang)
                (2) Passwort (mind. 8 Zeichen, mind. 1 Klein- und Großbuchstabe, mind. 1 Zahl, mind. 1 Sonderzeichen)
                (3) Benutzername/eMail (muss eindeutig sein – in der DB-Tabelle)
                (4) Geburtsdatum: kein Pflichtfeld, wird aber etwas ausgewählt, dann muss das Datum in der Vergangenheit liegen
                (5) Fehlermeldungen in der View anzeigen
            iii) in DB abspeichern
                (1) Passwort soll als Hash abgespeichert werden (Klasse PasswortHasher<T> verwenden)
            iv) Meldung an Benutzer (Erfolg/Misserfolg)
                (1) eigene _Message.cshtml-View erzeugen
                (2) zugehörige Message-Klasse erzeugen
        d) Eingabedaten in der DB überprüfen
        e) Test: versuche einen weiteren Benutzer, mit einem bereits vorhandenen Benutzernamen, zu registrieren => Ausgabe überprüfen
        f) TODO: DI (Dependency Injection) erklären und einbauen (z.B. DbManager)
    2) Login
        a) Login-View erzeugen
        b) Login-Methode programmieren
        c) Meldung an den Benutzer ausgeben (Erfolg/Misserfolg)
        d) Sessions: wird später hinzugefügt (damit der Server den eingeloggten User bei weiteren Requests erkennt)
        e) Logout programmieren
        f) TODO: Sessions einbauen

    3) Shop
        a) Artikel-View (alle Artikel anzeigen) erzeugen
            i) ShopController erzeugen
            ii) View: um alle Artikel anzuzeigen (vorerst nur eine Meldung anzeigen)
            iii) Klasse: Article (ArticleId, Name, Description, Price, Brand, ArticleType (enum)) erzeugen
            iv) DB
                (1) dem DB-Manager bekanntgeben
                (2) Migration erzeugen & DB aktualisieren
                (3) DB auf Richtigkeit überprüfen
                (4) in die DB-Tabelle mehrere Artikel von Hand eintragen
            v) in der Controllermethode (i) alle Artikel aus der DB laden und an die View übergeben
            vi) zugehörige View: alle Artikel sauber gestaltet anzeigen
        b) einen neuen Artikel anlegen (für Admin)
            i) Controllermethode (Formularanzeigen) erzeugen
            ii) View (Formular für die Artikeleingabe) erzeugen
            iii) Controllermethode (Formularauswertung) erstellen
                (1) Formularauswertung
                (2) In DB abspeichern
                (3) Meldung an den Benutzer ausgeben
