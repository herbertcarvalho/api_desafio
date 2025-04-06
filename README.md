🛒 Sistema de E-commerce - Cadastro e Gestão
Este projeto de e-commerce foi desenvolvido utilizando o padrão Repository, com a separação clara de responsabilidades através do uso de Commands e MediatR para orquestração das ações.

👨‍💻 Sobre o Projeto
Este é um sistema de e-commerce voltado para o cadastro e gerenciamento de produtos, categorias e usuários. Foi desenvolvido com ASP.NET e ASP.NET Identity, garantindo uma estrutura segura e escalável para evolução futura.
Todas as portas das imagens utilizadas no docker-compose estão externas.

🔐 Autenticação e Autorização
Utiliza ASP.NET Identity para controle de acesso
Cadastro e login de usuários
Restrições de acesso a funcionalidades específicas para usuários autenticados.

⚙️ Funcionalidades
Cadastro de produtos
Cadastro de categorias
Cadastro e gerenciamento de usuários
Login com proteção de senha e política de autenticação
Logs de todas operações

🛠️ Tecnologias Utilizadas
ASP.NET Core
ASP.NET Identity
Entity Framework Core
C#
SQL Server
MinIo
MongoDb

🧠 Regras de Negócio
Apenas usuários autenticados podem acessar o sistema
Existem dois tipos de usuário : 
  Cliente: Ele pode apenas pesquisar sobre os produtos, criado no cadastro quando não é fornecido um cnpj.
  Empresa: Pode realizar todas operações , criado no cadastro quando é fornecido um cnpj.

Sobre as categorias:
  Uma categoria pode ser alterada ou removida por qualquer usuário do tipo "Empresa".
  A categoria só pode ser removida caso não esteja vinculada a algum produto.

Sobre os produtos:
  Ao criar um produto , o produto ficará vinculado a empresa do usuário .
  Ao Apagar ou atualizar um produto , é necessário que esteja logado com o usuário daquela empresa.

🚀 Como Rodar o Projeto
1- Clone o repositório
2- Certifique-se que o docker está em sua máquina.
3- docker compose up -d --build

Segue os link's utilizaveis para conexão externa com o projeto:
MONGO DB: "mongodb://root:example@localhost:27017/"
MINIO:
  API : "localhost:9000"
  UI :  "localhost:9001"
POSTGRESQL : "Host=localhost;Port=5432;Database=mydatabase;Username=myuser;Password=mypassword;"
  
  
