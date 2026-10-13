import random;
from datetime import datetime, timedelta
class Generator_Dat:
    def __init__(self, data_start, liczba_dni):
        self.data_start = datetime.strptime(data_start, "%Y-%m-%d")
        self.liczba_dni_zakresu = liczba_dni
        self.daty = set()
        self.slownik = dict()
        self.weekend = 0

    def generuj_daty(self):
        lista_liczb = set()
        while len(lista_liczb) < 1000:
            lista_liczb.add(random.randint(0,self.liczba_dni_zakresu))
        self.daty = [self.data_start + timedelta(days=dzien) for dzien in lista_liczb]

    def analiza(self):
        self.weekend = sum(1 for d in self.daty if d.weekday() >= 5)
        nazwy_ = {
            0 : "Monday",
            1 : "Tuesday",
            2 : "Wednesday",
            3 : "Thursday",
            4 : "Friday",
            5 : "Saturday",
            6 : "Sunday"
        }
        self.slownik = {nazwa: 0 for nazwa in nazwy_.values()}
        for data in self.daty:
            self.slownik[nazwy_[data.weekday()]] += 1
        
    def wypisz(self):
            print(", ".join([data.strftime("%Y-%m-%d") for data in self.daty]))
            print(f"wWeekend: {self.weekend}")
            print(f"dniTygodnia: {self.slownik}")
    def zapisz(self):
        with open("daty.txt", "w", encoding="utf-8") as file:
            file.write(f'{", ".join([data.strftime("%Y-%m-%d") for data in self.daty])}\n')
            file.write(f"wWeekend: {self.weekend}\n")
            file.write(f"dniTygodnia: {self.slownik}")

def main():
    gen = Generator_Dat("2020-01-01",2500)
    gen.generuj_daty()
    gen.analiza()
    gen.wypisz()
    gen.zapisz()
if  __name__ == "__main__":
    main()