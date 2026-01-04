# IM2B - Sistema de Gestão de Filmes e Atores

## Grupo
**Integrantes:**
- Diego Mayrink: responsável pelos controladores (e autenticação)
- Rui Santos: responsável pela base de dados e repositorios
- Sergio Sant'Anna: responsável pelos models e viewmodels
- Talita Santos: responsável pelas views e frontend

---

## Descrição do Projeto
Projeto desenvolvido para a UC00608, formador Tiago Filipe Borges baseado num Sistema web desenvolvido em ASP.NET Core MVC para gestão de filmes, atores e seus papéis. Permite cadastro, edição, visualização e exclusão de filmes e atores, além de controle de acesso com autenticação de usuários.

---

## Requisitos do Sistema
- .NET 8.0 SDK
- SQL Server (Express ou superior)
- Visual Studio 2022 ou superior (recomendado)

---

## Passo a Passo para Configuração

### 1. Configurar a Connection String

Abra o arquivo **`IM2B/appsettings.json`** e configure a connection string do seu SQL Server:

```json
{
  "ConnectionStrings": {
    "ContainerConnection": "Server=SEU_SERVIDOR;Database=IM2B;User Id=SEU_USUARIO;Password=SUA_SENHA;Encrypt=True;TrustServerCertificate=True;",
    "SergioConnection": "Data Source=SEU_SERVIDOR\\SQLEXPRESS;Database=IM2B;Trusted_Connection=True;TrustServerCertificate=True;",
    "TalitaConnection": "Data Source=SEU_SERVIDOR\\SQLEXPRESS;Database=IM2B;Persist Security Info=False;User ID=sa;Password=SUA_SENHA;Encrypt=False;"
  }
}
```

**Exemplo com Windows Authentication (recomendado):**
```json
"MinhaConnection": "Data Source=.\\SQLEXPRESS;Database=IM2B;Trusted_Connection=True;TrustServerCertificate=True;"
```

**Exemplo com SQL Server Authentication:**
```json
"MinhaConnection": "Server=localhost;Database=IM2B;User Id=sa;Password=SuaSenha123!;Encrypt=True;TrustServerCertificate=True;"
```

### 2. Criar a Base de Dados

Abra o **Package Manager Console** no Visual Studio:
- Menu: `Tools` > `NuGet Package Manager` > `Package Manager Console`
- Selecione o projeto **`context`** como Default Project
- Execute os seguintes comandos:

```powershell
Add-Migration InitialCreate
Update-Database
```

### 3. Executar o Projeto

1. Defina o projeto **`IM2B`** como projeto de inicialização (clique com botão direito no projeto > Set as Startup Project)
2. Pressione **F5** ou clique em **Run**
3. Ao iniciar, será solicitado no console qual connection string usar (opções 1, 2 ou 3)
4. A aplicação criará automaticamente:
   - As roles (Curador e Utilizador)
   - O usuário administrador (Curador)
   - Filmes e atores de exemplo

---

## Credenciais de Acesso

### Administrador (Curador)
- **Email:** `curador@email.pt`
- **Senha:** `Curador123!`
- **Permissões:** Acesso total ao sistema (criar, editar e apagar filmes/atores)

### Registrar Novo Utilizador
- Acesse a página de registro no menu
- Novos usuários recebem o papel de "Utilizador" (apenas visualização)

---

## Funcionalidades

- **Filmes:** Listar, criar, editar, visualizar detalhes e apagar
- **Atores:** Listar, criar, editar, visualizar detalhes e apagar
- **Papéis:** Associar atores a filmes com personagens específicos
- **Autenticação:** Sistema de login com controle de acesso por roles
- **Busca:** Sistema de pesquisa de filmes e atores

---

## Estrutura do Projeto

```
IM2B/
├── IM2B/              # Projeto Web (Controllers, Views)
├── context/           # Contexto do Entity Framework, Repositories e Seeders
├── shared/            # Models e Interfaces compartilhadas
└── README.md          # Este arquivo
```

---

## Tecnologias Utilizadas

- ASP.NET Core MVC 8.0
- Entity Framework Core 8.0
- SQL Server
- Bootstrap (Frontend)
- ASP.NET Core Identity (Autenticação)

---

## Problemas Comuns

### Erro de conexão com o banco de dados
- Verifique se o SQL Server está em execução
- Confirme se a connection string está correta
- Verifique as credenciais de acesso

### Erro ao executar migrations
- Certifique-se de que selecionou o projeto **`context`** no Package Manager Console
- Delete a pasta `Migrations` e execute novamente `Add-Migration` e `Update-Database`

### Não consegue fazer login
- Verifique se executou o `Update-Database` corretamente
- Certifique-se de que a aplicação iniciou sem erros no console (criação do Curador)

---

## Observações
- O sistema cria automaticamente dados de exemplo (filmes e atores) na primeira execução
- A base de dados será criada automaticamente ao executar as migrations
- Certifique-se de que o SQL Server permite conexões TCP/IP se usar SQL Authentication
