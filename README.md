**************************************************************
CRIAÇÃO DO BANCO DE DADOS
**************************************************************
-Criar um banco de dados "DB_ENQUETE" no SQL server 2018.

-Executar o script "Script Banco" dentro do Banco de dados criado.


**************************************************************
STRING DE CONEXÃO
**************************************************************

-Deve se subistituir a string de conexão com o banco de acordo com o local que esta localizado.

-Local para substituição:

	-Desafio Enquete\Desafio_Database\Conexao.cs
	
	-linha -> string StrConexao = (@"data source=localhost\SQLSERVER;Integrated Security=SSPI;Initial Catalog=DB_ENQUETE");


**************************************************************
URL BASE PARA COMUNICAÇÃO COM O API
**************************************************************

-Alterar a URL base de conexão com o API de acordo com a publicação no servidor.

-Local para substituição:

	-Desafio Enquete\\Web_UI\Controllers\PollController.cs
	
	-linha -> string Baseurl = "https://localhost:44371/api/poll/";


**************************************************************
PUBLICANDO O PROJETO
**************************************************************

-Abra a Solução "Desafio" no Visual Studio 2019.

-Compile a Solução.

-Publique o Projeto WebAPI;

-Publique o Projeto WEB_UI;


Copie as publicações para o servidor e configure o IIS.

Lenbrando que a URL BASE deve ser igual a URL configurada no IIS.
