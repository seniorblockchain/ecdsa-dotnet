using Xunit;
using EllipticCurve;

namespace EcdsaDotNetTests {

    public class TestOpenSsl : IClassFixture<PathFixture>{


        [Fact]
        public void testAssign() {
    
            // Generated by: openssl ecparam -name secp256k1 -genkey -out privateKey.pem
            string privateKeyPem = EllipticCurve.Utils.File.read("files/privateKey.pem");
            PrivateKey privateKey = PrivateKey.fromPem(privateKeyPem);

            string message = EllipticCurve.Utils.File.read("files/message.txt");

            Signature signature = Ecdsa.sign(message, privateKey);

            PublicKey publicKey = privateKey.publicKey();

            Assert.True(Ecdsa.verify(message, signature, publicKey));
        }

        [Fact]
        public void testVerifySignature() {

            // openssl ec -in privateKey.pem -pubout -out publicKey.pem
            string publicKeyPem = EllipticCurve.Utils.File.read("files/publicKey.pem");

            // openssl dgst -sha256 -sign privateKey.pem -out signature.binary message.txt
            byte[] signatureDer = EllipticCurve.Utils.File.readBytes("files/signatureDer.txt");

            string message = EllipticCurve.Utils.File.read("files/message.txt");

            PublicKey publicKey = PublicKey.fromPem(publicKeyPem);
            Signature signature = Signature.fromDer(signatureDer);

            Assert.True(Ecdsa.verify(message, signature, publicKey));
        }

    }

}
