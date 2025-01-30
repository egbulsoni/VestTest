# VestTest API

API para gerenciamento de inscrições, candidatos, ofertas de curso e processos seletivos.

## Endpoints

### Inscrições

- **GET /api/inscricao**: Retorna todas as inscrições.
- **GET /api/inscricao/{id}**: Retorna uma inscrição pelo ID.
- **POST /api/inscricao**: Cria uma nova inscrição.
- **PUT /api/inscricao/{id}**: Atualiza uma inscrição existente.
- **DELETE /api/inscricao/{id}**: Exclui uma inscrição pelo ID.
- **GET /api/inscricao/cpf/{cpf}**: Retorna inscrições por CPF.
- **GET /api/inscricao/oferta/{ofertaCursoId}**: Retorna inscrições por oferta de curso.

### Ofertas de Curso

- **GET /api/ofertacurso**: Retorna todas as ofertas de curso.
- **GET /api/ofertacurso/{id}**: Retorna uma oferta de curso pelo ID.
- **POST /api/ofertacurso**: Cria uma nova oferta de curso.
- **PUT /api/ofertacurso/{id}**: Atualiza uma oferta de curso existente.
- **DELETE /api/ofertacurso/{id}**: Exclui uma oferta de curso.

### Candidatos

- **GET /api/candidato**: Retorna todos os candidatos.
- **GET /api/candidato/{id}**: Retorna um candidato pelo ID.
- **POST /api/candidato**: Cria um novo candidato.
- **PUT /api/candidato/{id}**: Atualiza um candidato existente.
- **DELETE /api/candidato/{id}**: Exclui um candidato.

### Processos Seletivos

- **GET /api/processoseletivo**: Retorna todos os processos seletivos.
- **GET /api/processoseletivo/{id}**: Retorna um processo seletivo pelo ID.
- **POST /api/processoseletivo**: Cria um novo processo seletivo.
- **PUT /api/processoseletivo/{id}**: Atualiza um processo seletivo existente.
- **DELETE /api/processoseletivo/{id}**: Exclui um processo seletivo.

## Como Rodar a API Localmente

1. **Clonar o repositório:**
   ```bash
   git clone https://github.com/egbulsoni/VestTest.git
   ```

2. **Restaurar dependências:**
   ```bash
   cd .\VestTest\VestTest
   dotnet restore
   ```

3. **Construir o projeto:**
   ```bash
   dotnet build
   ```

4. **Executar a API:**
   ```bash
   dotnet run
   ```

Acesse a API em `http://localhost:5000`.

---

## Tecnologias

- .NET 5
- Entity Framework Core
- MySQL

---

## Licença

Este projeto está licenciado sob a [MIT License](https://opensource.org/licenses/MIT).
