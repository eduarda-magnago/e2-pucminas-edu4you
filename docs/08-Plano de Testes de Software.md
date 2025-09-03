# Plano de Testes de Software

Este documento apresenta o Plano de Testes de Software para o sistema Edu4You, com o objetivo de garantir que a aplicação esteja em conformidade com os requisitos especificados e atenda às expectativas dos usuários.

A seguir, são descritos os casos de teste:
 
| **Caso de Teste** 	| **CT01 – Fomulário de cadastro/login** 	|
|:---:	|:---:	|
|	Requisito Associado 	| RF-001 - A aplicação deverá conter uma página inicial da Edu4you com formulário de cadastro/login. |
| Objetivo do Teste 	| Verificar se a página inicial contém o formulário de cadastro/login. |
| Passos 	| - Acessar o navegador <br> - Informar o endereço do site <br> - Verificar se há um formulário de login/cadastro visível <br> - Preencher os campos de cadastro e criar uma conta <br> - Preencher os campos de login, senha e acessar <br> - Tentar realizar login com dados válidos e inválidos |
|Critério de Êxito | Página deve apresentar corretamente os formulários e permitir o cadastro e login com validação adequada. |
|  	|  	|
| **Caso de Teste** 	| **CT02 – Cadastrar perfil de aluno** 	|
|Requisito Associado | RF-002	- A aplicação deve permitir ao usuário cadastrar um perfil de aluno e fazer o login na página. |
| Objetivo do Teste 	| Garantir que um aluno possa se cadastrar e acessar sua conta. |
| Passos 	| - Acessar o navegador <br> - Informar o endereço do site <br> - Acessar a página de cadastro <br> - Preencher os campos obrigatórios (e-mail, nome, sobrenome, cpf, senha e confirmação de senha) <br> - Aceitar os termos de uso <br> - Clicar em "Cadastrar" <br> - Recarregar a página <br> - Clicar no botão "Login" <br> - Preencher campos solicitados <br> - Clicar em "Entrar"|
|Critério de Êxito | Cadastro deve ser concluído com sucesso e login validado corretamente. |
|  	|  	|
| **Caso de Teste** 	| **CT03 – Cadastrar perfil de professor** 	|
|	Requisito Associado 	| RF-003 - A aplicação deve permitir ao usuário cadastrar um perfil de professor e fazer o login na página. |
| Objetivo do Teste 	| Verificar se um professor pode se cadastrar e acessar sua conta. |
| Passos 	| - Acessar o navegador <br> - Informar o endereço do site <br> - Acessar a página de cadastro <br> - Preencher os campos obrigatórios (e-mail, nome, sobrenome, cpf, senha e confirmação de senha) <br> - Aceitar os termos de uso <br> - Clicar em "Cadastrar" <br> - Recarregar a página <br> - Clicar no botão "Login" <br> - Preencher campos solicitados <br> - Clicar em "Entrar"|
|Critério de Êxito | Cadastro deve ser concluído com sucesso e login validado corretamente. |
|  	|  	|
| **Caso de Teste** 	| **CT04 – Publicação de conteúdo em diferentes formatos**	|
|Requisito Associado | RF-004	- Os usuários devem conseguir publicar conteúdos em diferentes formatos (textos, vídeos, etc). |
| Objetivo do Teste 	| Verificar se a aplicação permite publicação de diferentes tipos de conteúdo. |
| Passos 	| - Acessar o navegador <br> - Informar o endereço do site <br> - Clicar no botão "Login" <br> - Preencher campos solicitados <br> - Clicar em "Entrar" <br> - Acessar a área de publicação de conteúdos <br> - Selecionar e fazer upload de diferentes tipos de arquivos (texto, vídeos, etc.). <br> - Clicar em "Publicar" |
|Critério de Êxito | Sistema deve aceitar os formatos permitidos e exibir os conteúdos publicados. |
|  	|  	|
| **Caso de Teste** 	| **CT05 – Fóruns de discussão**	|
|Requisito Associado | RF-005	- A aplicação deve conter fóruns de discussão com as dúvidas dos alunos. |
| Objetivo do Teste 	| Verificar se os fóruns funcionam corretamente. |
| Passos 	| - Acessar o navegador <br> - Informar o endereço do site <br> - Clicar em "Login" <br> - Preencher campos solicitados <br> - Clicar em "Entrar" <br> - Clicar em "Fóruns" <br> - Criar um novo tópico de discussão <br> - Responder um tópico existente <br> - Verificar se as respostas são exibidas corretamente |
|Critério de Êxito | As interações no fórum devem ser registradas e exibidas corretamente. |
|  	|  	|
| **Caso de Teste** 	| **CT06 – Sistema de avaliação** 	|
|	Requisito Associado 	| RF-006 - O sistema deve permitir que alunos avaliem conteúdos e professores, garantindo a qualidade do material. |
| Objetivo do Teste 	| Verificar se os alunos podem avaliar conteúdos e professores. |
| Passos 	| - Acessar o navegador <br> - Informar o endereço do site <br> - Clicar em "Login" <br> - Preencher campos solicitados <br> - Clicar em "Entrar" <br> - Acessar um conteúdo publicado <br> - Utilizar a funcionalidade de avaliação |
|Critério de Êxito | A avaliação deve ser salva e exibida corretamente. |
|  	|  	|
| **Caso de Teste** 	| **CT07 – Tópicos/categoria** 	|
|	Requisito Associado 	| RF-007 - A aplicação deverá conter uma listagem de todos os tópicos disponíveis por categoria. |
| Objetivo do Teste 	| Verificar se os tópicos estão organizados corretamente por categoria. |
| Passos 	| - Acessar o navegador <br> - Informar o endereço do site <br> - Clicar em "Login" <br> - Preencher campos solicitados <br> - Clicar em "Entrar" <br> - Acessar área de conteúdos <br> - Navegar entre diferentes categorias <br> - Conferir se os tópicos estão sendo exibidos corretamente em suas categorias. |
|Critério de Êxito | Listagem organizada por categorias deve estar funcional. |
|  	|  	|
| **Caso de Teste** 	| **CT08 – Gerenciamento de cursos pelo professor** 	|
|	Requisito Associado 	| RF-008 - Educadores devem ter liberdade para gerenciar seus próprios cursos e conteúdos. |
| Objetivo do Teste 	| Verificar se os professores conseguem gerenciar seus cursos. |
| Passos 	| - Acessar o navegador <br> - Informar o endereço do site <br> - Clicar em "Login" <br> - Preencher campos solicitados <br> - Clicar em "Entrar" <br> - Acessar painel de usuário como professor <br> - Criar, editar, remover cursos |
|Critério de Êxito | Sistema deve aceitar e aplicar as alterações com sucesso. |
|  	|  	|
| **Caso de Teste** 	| **CT09 – Gerenciamento de cursos pelo professor**	|
|Requisito Associado | RF-009	- A aplicação deve conter uma página de gerenciamento para que o professor consiga visualizar o desempenho na plataforma através de métricas. |
| Objetivo do Teste 	| Verificar a visualização de métricas de desempenho. |
| Passos 	| - Acessar o navegador <br> - Informar o endereço do site <br> - Clicar em "Login" <br> - Preencher campos solicitados <br> - Clicar em "Entrar" <br> - Acessar painel de usuário como professor <br> - Navegar até a página de métricas. <br> - Verificar apresentação de dados como visualizações, avaliações, etc. |
|Critério de Êxito | Página deve apresentar as informações com clareza e precisão. |
 |  	|  	|
| **Caso de Teste** 	| **CT10 – Exibição de Configurações**	|
|Requisito Associado | RF-010 - Os usuários possuem o controle de editar suas informações pessoais na aba de Configurações. <br> RF-011 - Os usuários conseguem visualizar sua página de Perfil com todas as suas informações salvas. |
| Objetivo do Teste 	| Verificar se o sistema exibe a aba configurações com informações gerais do usuário. |
| Passos 	| - Acessar o navegador <br> - Informar o endereço do site <br> - Clicar em "Login" <br> - Preencher campos solicitados <br> - Clicar em "Entrar" <br> - Acessar área de configurações <br> - Conferir se os tópicos estão sendo exibidos corretamente.  |
|Critério de Êxito | Listagem organizada de informações deve estar funcional.  |
