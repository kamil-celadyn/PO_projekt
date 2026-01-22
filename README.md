# System Ewidencji Pracowników i Obliczania Wynagrodzeń 💼

**Projekt zaliczeniowy: Programowanie Obiektowe** **Rok akademicki:** 2025/2026  

![Główny widok aplikacji](image_05c1d3.png)

## 📖 O projekcie

Aplikacja typu Desktop (WPF) służąca do obsługi działu kadr w małym przedsiębiorstwie. System umożliwia ewidencjonowanie pracowników różnych typów (etatowych oraz zleceniobiorców), automatyczne obliczanie ich wynagrodzeń zgodnie z typem umowy oraz trwały zapis danych.

Głównym celem projektowym było praktyczne zastosowanie paradygmatów **programowania obiektowego** (OOP), takich jak dziedziczenie, polimorfizm, hermetyzacja oraz obsługa wyjątków.

---

## 🛠 Metryka Projektu i Technologie

* **Język programowania:** C# (.NET 6.0 / 8.0)
* **Technologia interfejsu:** WPF (Windows Presentation Foundation)
* **Format zapisu danych:** JSON (`System.Text.Json`)
* **Środowisko programistyczne:** Visual Studio 2022
* **Wersja:** 1.0

---

## 🏗 Architektura Systemu

System został zaprojektowany zgodnie z wzorcami OOP i dzieli się na dwie główne warstwy:

![Diagram UML](image_05c13a.png)

### 1. Warstwa Logiki Biznesowej (Backend)
Odpowiada za przetwarzanie danych, walidację i obliczenia.
* **`Pracownik` (Klasa abstrakcyjna):** Definiuje wspólny interfejs dla wszystkich typów pracowników. Zawiera mechanizm walidacji numeru PESEL w setterze właściwości (hermetyzacja).
* **`PracownikEtatowy` i `Zleceniobiorca`:** Klasy pochodne implementujące specyficzne metody obliczania wynagrodzenia (`ObliczPensje`) – **polimorfizm**.
* **`SystemKadrowy`:** Klasa zarządzająca kolekcją (`List<T>`). Pełni rolę kontrolera – obsługuje dodawanie obiektów, zapobiega duplikatom oraz realizuje serializację danych.

### 2. Warstwa Prezentacji (Frontend)
* **`MainWindow.xaml`:** Definicja wyglądu aplikacji oparta na języku znaczników XAML (Grid layout).
* **`MainWindow.xaml.cs` (Code-behind):** Obsługuje zdarzenia interfejsu (kliknięcia przycisków, wybór z listy rozwijanej) i komunikuje się z obiektem `SystemKadrowy`.

---

## 💾 Struktura Danych i Plików

Dane przechowywane są lokalnie w pliku `baza_kadrowa.json`. Serializacja odbywa się przy użyciu biblioteki `System.Text.Json` z opcją `WriteIndented = true`, co zapewnia czytelność pliku dla człowieka.

Dzięki atrybutowi `[JsonDerivedType]` w klasie bazowej, system automatycznie rozpoznaje typ obiektu (polimorficzna deserializacja).

**Przykładowa struktura zapisu (JSON):**
```json
[
  {
    "$type": "etat",
    "PensjaZasadnicza": 5000.0,
    "Imie": "Jan",
    "Nazwisko": "Kowalski",
    "Pesel": "90010112345"
  },
  {
    "$type": "zlecenie",
    "StawkaGodzinowa": 50.0,
    "LiczbaGodzin": 160,
    "Imie": "Anna",
    "Nazwisko": "Nowak",
    "Pesel": "92030354321"
  }
]
```
## ⚠️ Obsługa Błędów

System wykorzystuje własną klasę wyjątków `KadryException` do zgłaszania błędów logiki biznesowej. Wyjątki te są przechwytywane w warstwie GUI i prezentowane użytkownikowi w formie okien dialogowych `MessageBox`.

**Obsługiwane przypadki:**
* Próba dodania pracownika z błędnym PESEL (inna długość niż 11 znaków).
* Próba ustawienia ujemnej pensji lub stawki.
* Próba dodania duplikatu pracownika.

---

## 👥 Zespół Projektowy

| Imię i Nazwisko | Rola | Odpowiedzialność |
| :--- | :--- | :--- |
| **Kamil Celadyn** | Backend Dev | Abstrakcyjna logika biznesowa (`Pracownik`, `SystemKadrowy`), dziedziczenie, polimorfizm, serializacja JSON. |
| **Oskar Fryc** | Frontend Dev | Projekt i implementacja interfejsu graficznego (WPF - `MainWindow.xaml`), logika interakcji użytkownika. |
| **Mykhailo Bondar** | QA & Docs | Implementacja obsługi błędów (`KadryException`), interfejsy (`IBonusowalny`), Diagram UML, dokumentacja. |

---

## 🚀 Instrukcja Obsługi (Szybki Start)

1. **Uruchomienie:** Otwórz plik wykonywalny `SystemWynagrodzen.exe` (wymagany .NET Runtime 6.0+).
2. **Dodawanie:** Wypełnij panel po prawej stronie (Imię, Nazwisko, PESEL). Wybierz typ umowy – formularz dostosuje pola automatycznie.
3. **Zapis:** Kliknij "Zapisz do pliku", aby zachować zmiany w `baza.json`.
4. **Sortowanie:** Użyj przycisku pod listą, aby posortować pracowników alfabetycznie (A-Z).

---
## 📄 Pełna Dokumentacja

Kompletna dokumentacja projektu, w tym sprawozdanie oraz instrukcje, znajduje się w załączonych plikach PDF:

* 📄 `Sprawozdanie.pdf`
* 📄 `Dokumentacja_Techniczna.pdf`
* 📄 `Instrukcja_Uzytkownika.pdf`

---
&copy; 2026 Zespół Projektowy. Wszelkie prawa zastrzeżone.
