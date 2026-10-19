# Créditos e referências

**Aluno:** Nicholas Thuesen.

## Base acadêmica

Projeto individual desenvolvido sobre o tutorial Introdução Rápida de Jogos Digitais do Insper. Enunciados originais preservados em `docs/Projeto_IntroUnity_Original.pdf` e `docs/Tutorial_Introducao_Rapida_Original.pdf`. Mecânica original: movimento com Rigidbody2D, coletáveis, prefabs, áudio, menu e fim de partida.

Referência do tutorial: https://slender-ceder-1da.notion.site/Tutorial-Introdu-o-R-pida-1a6d1ae1773780a7a59cf823a8b599ec?pvs=4

As extensões Crystal Escape e os testes automatizados foram preparados com assistência de IA (OpenAI Codex). O aluno deve revisar, compreender e apresentar o código e respeitar as regras da disciplina. Não há código de colegas incorporado nesta implementação.

## Arte e áudio de Crystal Escape

Sprites Explorer, Crystal, PatrolDrone, HunterDrone, Reactor e LabFloor: desenhos geométricos procedurais produzidos especificamente para este projeto. Música ExtractionLoop e efeitos Collect, Hit, Victory e Defeat: síntese original por osciladores e ruído, sem samples ou músicas baixados. Gerador reproduzível em `Tools/CreateOriginalAssets.py`; não requer dependência Python no jogo compilado.

## Recursos já presentes no tutorial

- `ribhavagrawal-coin-recieved-230517.mp3`: **coin recieved**, RibhavAgrawal, Pixabay. Origem verificada pelo título, autor e identificador do arquivo em 19/10/2026: https://pixabay.com/sound-effects/film-special-effects-coin-recieved-230517/. A página identifica uso sob a Pixabay Content License: https://pixabay.com/service/license-summary/. O arquivo original foi preservado; Crystal Escape usa o novo efeito procedural Collect.
- Fonte Liberation Sans / TextMesh Pro: SIL Open Font License 1.1; texto da licença já presente em `Assets/TextMesh Pro/Fonts/LiberationSans - OFL.txt`. Copyright Google e Red Hat conforme o arquivo original.
- Unity, Universal Render Pipeline, Input System e TextMesh Pro: pacotes oficiais Unity, sujeitos aos termos e avisos de seus respectivos pacotes. Assets básicos originais do tutorial foram preservados e estendidos nos mesmos prefabs.
- Unity MCP (CoplayDev): ferramenta de automação do Editor, registrada em `Packages/manifest.json` e `Packages/packages-lock.json`; não é uma mecânica do jogo.

Não se atribui licença de conteúdo de terceiros aos scripts, arte e áudio procedurais novos. As licenças de fontes e do áudio original continuam sendo as de seus titulares.
