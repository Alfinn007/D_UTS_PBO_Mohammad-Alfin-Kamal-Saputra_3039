using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Pilih Metode :");
        Console.WriteLine("1. Setor");
        Console.WriteLine("2. Transfer");
        Console.Write("Masukkan Pilihan : ");
        string pilihan = Console.ReadLine();

        Console.Write("Masukkan nama Nasabah: ");
        string nasabah = Console.ReadLine();

        Console.Write("Masukkan Password : ");
        string password = Console.ReadLine();

        Console.Write("Masukkan Saldo Awal: ");
        if (!double.TryParse(Console.ReadLine(), out double saldo))
        {
            Console.WriteLine("Saldo harus berupa angka.");
            return;
        }

        Bank bank;
        switch (pilihan)
        {
            
            case "1":
                bank = new Setor(nasabah, password, saldo);
                Console.Write("Masukkan jumlah yang ingin disetor: ");
                if (double.TryParse(Console.ReadLine(), out double jumlahSetor))
                {
                    ((Setor)bank).ProsesSetor(jumlahSetor);
                }
                else
                {
                    Console.WriteLine("Jumlah setor harus berupa angka.");
                }
                break;
            case "2":
                bank = new Transfer(nasabah, password, saldo);
                Console.Write("Masukkan nama penerima: ");
                string penerima = Console.ReadLine();
                Console.Write("Masukkan jumlah yang ingin ditransfer: ");
                if (double.TryParse(Console.ReadLine(), out double jumlahTransfer))
                {
                    ((Transfer)bank).ProsesTransfer(penerima, jumlahTransfer);
                }
                else
                {
                    Console.WriteLine("Jumlah transfer harus berupa angka.");
                }
                break;
            default:
                Console.WriteLine("Pilihan tidak valid.");
                return;
        }

        Console.WriteLine($"Saldo Total: {bank.SaldoTotal()}");
    }
}

class Bank
{
    private string nasabah;
    private string password;
    private double saldo;

    public Bank(string nasabah, string password, double saldo)
    {
        this.nasabah = nasabah;
        this.password = password;
        this.saldo = saldo;
    }

    public string Nasabah
    {
        get { return nasabah; }
        set { nasabah = value; }
    }

    public string Password
    {
        get { return password; }
        set { password = value; }
    }

    public double Saldo
    {
        get { return saldo; }
        set { saldo = value; }
    }

    public virtual double SaldoTotal()
    {
        return Saldo;
    }
}

class Setor : Bank
{
    public Setor(string nasabah, string password, double saldo) : base(nasabah, password, saldo) { }

    public void ProsesSetor(double jumlah)
    {
        if (jumlah > 0)
        {
            Saldo += jumlah;
            Console.WriteLine($"Berhasil menyetor {jumlah}. Saldo saat ini: {Saldo}");
        }
        else
        {
            Console.WriteLine("Jumlah setor harus lebih dari 0.");
        }
    }
}

class Transfer : Bank
{
    public Transfer(string nasabah, string password, double saldo) : base(nasabah, password, saldo) { }

    public void ProsesTransfer(string penerima, double jumlah)
    {
        if (jumlah > 0 && jumlah <= Saldo)
        {
            Saldo -= jumlah;
            Console.WriteLine($"Berhasil mentransfer {jumlah} ke {penerima}. Saldo saat ini: {Saldo}");
        }
        else if (jumlah > Saldo)
        {
            Console.WriteLine("Saldo tidak mencukupi untuk transfer.");
        }
        else
        {
            Console.WriteLine("Jumlah transfer harus lebih dari 0.");
        }
    }
}
