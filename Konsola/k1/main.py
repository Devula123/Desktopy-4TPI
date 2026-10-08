class Produkt():
    def __init__(self, nazwa,cena_netto,ilosc):
        self.nazwa = nazwa
        self.cena_netto = cena_netto
        self.ilosc = ilosc
        
    def oblicz_wartosc_brutto(self,vat=23):
        return self.cena_netto * (1 + (vat/100))
    
    def oblicz_wartosc_magazynu_brutto(self):
        return self.ilosc * self.oblicz_wartosc_brutto()
    
    def  wyswietl_opis(self):
        print(f"----------------------------------------\n" \
        f"Produkt: {self.nazwa}\n" \
        f"Ilość na stanie: {self.ilosc} szt.\n" \
        f"Cena netto: {self.cena_netto:.2f} PLN | Cena brutto: {self.oblicz_wartosc_brutto():.2f} PLN\n" \
        f"Łączna wartość w magazynie (brutto): {self.oblicz_wartosc_magazynu_brutto():.2f} PLN\n" \
        f"----------------------------------------")

def main():
    # lista = list()
    # with open("produkty.txt", "+r", encoding="utf-8") as file:
    #     for prod in file.readlines():
    #         prod_ = prod.split(",")
    #         lista.append(Produkt(prod_[0],float(prod_[1]),int(prod_[2])))
    # suma = 0
    # for prod in lista:
    #     prod.wyswietl_opis()
    #     suma += prod.oblicz_wartosc_magazynu_brutto()
    # print(f"ŁĄCZNA WARTOŚĆ CAŁEGO MAGAZYNU (BRUTTO): {suma:.2f} PLN")
    try:
        suma = 0
        with open("produkty.txt", "+r", encoding="utf-8") as file:
            for prod in file.readlines():
                prod_ = prod.split(",")
                pro = Produkt(prod_[0],float(prod_[1]),int(prod_[2]))
                pro.wyswietl_opis()
                suma+= pro.oblicz_wartosc_magazynu_brutto()
        print(f"ŁĄCZNA WARTOŚĆ CAŁEGO MAGAZYNU (BRUTTO): {suma:.2f} PLN")
    except(FileNotFoundError):
        print("Plik nie istnieje")
    except(Exception):
        print("Blad wczytywania")
if __name__ == "__main__":
    main()