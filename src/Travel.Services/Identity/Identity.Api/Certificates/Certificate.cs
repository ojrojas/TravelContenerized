namespace Identity.Api.Certificates;

public class Certificate
{
    public static X509Certificate2 GetCert()
    {
        var assembly = typeof(Certificate).GetTypeInfo().Assembly;

        using var stream = assembly.GetManifestResourceStream("Identity.Api.Certificates.Identity.Api.Certificates.Travel.pfx");
        if (stream is null) throw new InvalidOperationException("No found stream file certicate");
        return new X509Certificate2(ReadStream(stream), "idsTravel");
    }

    private static byte[] ReadStream(Stream stream)
    {
        byte[] buffer = new byte[16 * 1024];
        using MemoryStream ms = new MemoryStream();
        int read;
        while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
        {
            ms.Write(buffer, 0, read);
        }

        return ms.ToArray();
    }
}

