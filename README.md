# DataPacker

Запаковка:
```Csharp
data.Pack(new Catalog()
{
    TxtFile = "ETOT TEKST TOJE ZAPAKOVAN",
    Picture = new byte[] { 0x01, 0x02, 0x03 },
    SoftwareName = new string[] { "pervi", "vtoroi", "treti" }
}, "res.ovd");
```

Распаковка:
```Csharp
/* передается путь к файлу который необходимо распаковать */
var res = data.Unpack("res.ovd"); 
```