using QRCoder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/MiCodigoQR", () =>
{
    //... Configuración básica para un código QR.
    string datosPrueba = "GERSON SANTOS M..";
    QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();
    var qrCodeData = qRCodeGenerator.CreateQrCode(datosPrueba, QRCodeGenerator.ECCLevel.Q);
    BitmapByteQRCode bitmapByteQR = new BitmapByteQRCode(qrCodeData);
    var bitMap = bitmapByteQR.GetGraphic(20);

    //... Conversion del codigo QR a base64
    var ms = new MemoryStream();
    ms.Write(bitMap);
    byte[] resultadoQR = ms.ToArray();
    return Convert.ToBase64String(resultadoQR);
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();
