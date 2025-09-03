# Especificações do Projeto
## Personas

|Imagem    | Nome  | Descrição |
|------|-----------------------------------------|----|
|<img width="419" alt="Joao Silva" src="https://github.com/user-attachments/assets/f7792138-b489-4284-8681-7aebe9ba0872" />| João Souza | **Idade**: 18 anos; **Localização**: Periferia de São Paulo; **Perfil**: Estudante de ensino médio, sonha em cursar programação, mas não tem condições de pagar cursos tradicionais. Acessa a internet apenas pelo celular.; **Dores**: Pela internet instável e falta de dispositivos adequados, João tem acesso limitado a grande parte dos recursos online, como vídeos longos ou tutoriais que requeiram computador ou notebook.; **Expectativas**: Encontrar uma plataforma que ofereça conteúdo leve, com opções de trilhas e cursos que possam ser realizados completamente pelo celular, além de conectar com outros alunos para poder discutir e compartilhar dúvidas, ou até comunicar diretamente com o professor. | 
|<img width="424" alt="Maria Souza" src="https://github.com/user-attachments/assets/411f99bc-73a4-4351-a52e-1677554ea615" />| Maria Souza| **Idade**: 34 anos; **Localização**: Zona rural da Bahia; **Perfil**: Professora voluntária em uma comunidade agrícola, onde ela ensina habilidades digitais básicas para adultos e jovens.; **Dores**: Maria sofre com a falta de infraestrutura digital na região, além da dificuldade de engajar alunos com pouca familiaridade digital.; **Expectativas**: Encontrar ferramentas que facilitem a criação de materiais de aprendizagem interativos para seus alunos, além de compartilhar experiências com educadores de outras regiões. |
|<img width="427" alt="Carlos Mendes" src="https://github.com/user-attachments/assets/12068637-abb3-4eb1-a10c-073490124882" />| Carlos Mendes | **Idade**: 68 anos; **Localização**: Porto Alegre; **Perfil**: Aposentado que quer aprender tecnologia para se conectar com a família. Tem smartphone, porém pouca familiaridade com apps complexos.; **Dores**: Carlos não tem ninguém disponível para ensiná-lo de forma efetiva, e tem dificuldade em acompanhar ritmos acelerados de ensino.; **Expectativa**: Encontrar um aplicativo que proporcione vídeos curtos com explicações passo a passo que seja intuitivo e fácil de utilizar, além de conectar com outros alunos em situações semelhantes. |
|<img width="422" alt="Ana Lucia Fernandes" src="https://github.com/user-attachments/assets/bb5f8fd1-cfe9-4a30-9b15-3e59f61dd2fe" />| Ana Lúcia Fernandes | **Idade**: 28 anos; **Localização**: Belo Horizonte; **Perfil**: Professora de Inglês e redação, oferece aulas particulares. Quer expandir sua visibilidade criando cursos online.; **Dores**: Ana Lúcia enfrenta dificuldades em elaborar conteúdos interativos e conectar com alunos de cursos nas demais plataformas online.; **Expectativa**: Encontrar uma plataforma que forneça ferramentas para construção de cursos e trilhas multimídia, podendo criar tarefas e provas que serão avaliadas diretamente pelo professor. |
|<img width="424" alt="Pedro Henrique" src="https://github.com/user-attachments/assets/2269d021-84ca-4741-bedb-2f71257acf9a" />| Pedro Henrique | **Idade**: 22 anos; **Localização**: Rio de Janeiro; **Perfil**: Estudante de Engenharia que sonha em seguir a carreira acadêmica na área matematica.; **Dores**: Pedro não tem experiência em sala de aula ou com ensino formal/informal, além da dificuldade de encontrar alunos empenhados.; **Expectativa**: Encontrar uma plataforma onde possa elaborar um perfil de professor e encontrar alunos interessados em matematica. Além disso, poder utilizar a mesma plataforma para encontrar professores referência na área. |



## Histórias de Usuários

Com base na análise das personas forma identificadas as seguintes histórias de usuários:

|EU COMO... `PERSONA`| QUERO/PRECISO ... `FUNCIONALIDADE` |PARA ... `MOTIVO/VALOR`                |
|----------------------|--------------------------------------------------------------------------|---------------------------------------------------------------|
|João Silva            | Poder encontrar aulas sobre temas específicos com materiais de qualidade | Aprender de forma estruturada com especialistas na área |
|Pedro Henrique        | Publicar meus conteúdos e apresenta-los para alunos interessados         | Angariar experiência em tutoria.|
|Carlos Mendes         | Visualizar avaliações e feedbacks de outros estudantes antes de acompanhar um curso | Sentir-me seguro de que o conteúdo é relevante e de qualidade |
|Carlos Mendes         | Conseguir aprender de modo online mantendo alta qualidade                | Evitar ter de me deslocar grandes distãncias para fazer minhas aulas |
|Maria Souza           | Conectar-me com outros professores ou profissionais com expertise em minha área de ensino | Garantir que estou mantendo-me atualizado |
|Maria Souza           | Poder oferecer diferentes formatos de conteúdo                           | Tornar minha aula mais atrativa e dinâmica |
|Ana Lúcia Fernandes   | Definir abordagens diferenciadas para meus diferentes conteúdos          | Atingir diferentes perfis de alunos |
|João Silva            | Acessar meu histórico de conteúdos adquiridos e estudados                | Organizar melhor o meu ritmo de aprendizado |
|Carlos Mendes         | Criar um ritmo de estudos que seja confortável a minha rotina            | Evitar procrastinação ou desmotivação para com os estudos |
|Ana Lúcia Fernandes   | Acompanhar estatísticas sobre o desempenho dos meus cursos juntamente com feedbacks | Entender o que está funcionando bem e quais os pontos de melhoria |
|João Dias             | Encontrar um professor particular que me auxilie em meus estudos         | Aumentar o meu ritmo de aprendizagem |


## Requisitos

As tabelas que se seguem apresentam os requisitos funcionais e não funcionais que detalham o escopo do projeto.

### Requisitos Funcionais

|ID    | Descrição do Requisito  | Prioridade |
|------|-----------------------------------------|----|
|RF-001| A aplicação deverá conter uma página inicial da Edu4you com formulário de cadastro/login. | Alta | 
|RF-002| A aplicação deve permitir ao usuário cadastrar um perfil de aluno e fazer o login na página.| Alta |
|RF-003| A aplicação deve permitir ao usuário cadastrar um perfil de professor e fazer o login na página. | Alta |
|RF-004| Os usuários devem conseguir publicar conteúdos em diferentes formatos (textos, vídeos, etc). | Alta |
|RF-005| A aplicação deve conter fóruns de discussão com as dúvidas dos alunos. | Média |
|RF-006| O sistema deve permitir que alunos avaliem conteúdos, garantindo a qualidade do material. | Alta |
|RF-007| A aplicação deverá conter uma listagem de todos os tópicos disponíveis por categoria. | Alta |
|RF-008| Educadores devem ter liberdade para gerenciar seus próprios cursos e conteúdos. | Média |
|RF-009| A aplicação deve conter uma página de gerenciamento para que o professor consiga visualizar o desempenho na plataforma através de métricas. | Alta |
|RF-010| Os usuários possuem o controle de editar suas informações pessoais na aba de Configurações. | Média |
|RF-011| Os usuários conseguem visualizar sua página de Perfil com todas as suas informações salvas. | Média |
|RF-012| Os usuários devem se matricular em um curso de sua escolha para poder visualizar os conteúdos disponíveis. | Alta |




### Requisitos Não Funcionais

|ID     | Descrição do Requisito  |Prioridade |
|-------|-------------------------|----|
|RNF-001| A aplicação deve ser responsiva para todos os tipos de tela. | Alta | 
|RNF-002| A plataforma deve suportar um grande número de usuários simultaneamente sem perda de desempenho. | Baixa | 
|RNF-003| Funcionar em diferentes dispositivos e sistemas operacionais (IOS, Android, etc). | Alta | 
|RNF-004| O tempo de carregamento das páginas, vídeos e outros materiais deve ser otimizado para não prejudicar a experiência de aprendizado. | Média | 


## Restrições

O projeto está restrito pelos itens apresentados na tabela a seguir.

|ID| Restrição                                             |
|--|-------------------------------------------------------|
|01| A equipe deve trabalhar em conjunto em todas as fases do projeto, promovendo a colaboração. |
|02| A comunicação entre os membros da equipe deve ser clara e eficiente, garantindo alinhamento em todas as fases do projeto.        |
|03| Todo o material utilizado no site deve ser autêntico ou originado de fontes de acesso público.        |
|04| O código-fonte deve ser bem estruturado e documentado para facilitar futuras modificações e colaborações.        |
|05| A criação do projeto deve ser feita com recursos e softwares gratuitos ou disponibilizados por meio de licenças educacionais.        |

## Diagrama de Casos de Uso

![Diagrama de casos de uso](img/casosdeuso.svg)
