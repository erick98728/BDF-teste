# AUTO_SCENE_BUILDER

## Objetivo
Gerar cenas jogaveis do Bosque da Nevoa Perdida com placeholders simples e os sistemas atuais do prototipo.

O builder mantem duas opcoes:
- `Prototype_Bosque_Test` para validar mecanicas em uma sala tecnica curta;
- `Prototype_Bosque_Demo` para testar uma demo maior com entrada, tutorial, combate, parkour, bifurcacao, Lucarelli, Dash e fim.

Nenhuma das cenas usa arte externa ou substitui a montagem final do mapa.

## Usar os menus
1. Abra o projeto no Unity Editor.
2. Espere a compilacao terminar.
3. Use um destes menus:
   - `Tools > Tester > Build Prototype Scene` para reconstruir `Assets/_Project/Scenes/Prototype_Bosque_Test.unity`;
   - `Tools > Tester > Build Bosque Demo Scene` para gerar ou reconstruir `Assets/_Project/Scenes/Prototype_Bosque_Demo.unity`.
4. Se houver cena alterada aberta, escolha se quer salva-la antes de o builder trocar a cena atual.

O builder tambem tenta:
- garantir as Tags `Player` e `Interactable`;
- garantir as Layers `Player`, `Ground`, `Enemy` e `Interactable`;
- incluir a cena gerada em Build Settings.

Se nao houver slot de Layer livre, o Console registra o aviso e a Layer correspondente precisa ser ajustada manualmente.

## Abrir a cena criada
Depois de rodar um menu, a cena correspondente ja fica aberta.

Para reabrir depois:
1. Use o Project window em `Assets/_Project/Scenes`.
2. Abra `Prototype_Bosque_Test` para a sala mecanica curta.
3. Abra `Prototype_Bosque_Demo` para a demo expandida.

Rodar o mesmo menu novamente reconstrui a cena automatica salva naquele caminho. A cena curta nao e removida quando a demo e gerada.

## Conteudo comum
### Rubens
- `Rubens_Player` com sprite placeholder, `Rigidbody2D`, `BoxCollider2D`, `PlayerController2D`, `PlayerHealth`, `PlayerCombat` e `AbilityManager`.
- Filho `GroundCheck` ligado ao controller.
- Filho `AttackPoint` ligado ao combate.
- Dash inicia bloqueado.

### Camera e UI
- `Main Camera` ortografica com `CameraFollow2D` apontando para Rubens.
- `GameManager` na cena.
- Canvas com HUD de vida, estado de Dash e mensagens.
- `PausePanel` simples com botoes `Continuar`, `Reiniciar` e `Sair`.
- `EventSystem` para clique dos botoes.

## Sala tecnica
`Prototype_Bosque_Test` continua sendo o teste rapido:
- piso principal e plataformas curtas na Layer `Ground`;
- dois inimigos basicos na Layer `Enemy`;
- um checkpoint trigger com `RespawnPoint`;
- uma barreira `AbilityGate`;
- Lucarelli sobre a mesma sala de validacao.

Use esta cena para verificar componentes, combate, respawn, HUD e pausa com pouco deslocamento.

## Demo expandida
`Prototype_Bosque_Demo` organiza a Hierarchy em:
- `Systems`;
- `Player`;
- `Camera`;
- `Level`;
- `Enemies`;
- `Checkpoints`;
- `Gates`;
- `Boss`;
- `Tutorial`;
- `DemoEnd`.

O fluxo montado pelo builder e:
1. Entrada do Bosque com spawn seguro e limite esquerdo.
2. Tutorial de movimento e pulo.
3. Area de combate basico com inimigos iniciais.
4. `Checkpoint_01_AfterCombat`.
5. Parkour basico com plataformas alcancaveis.
6. `Checkpoint_02_AfterParkour`.
7. Bifurcacao com `DashGate_Bifurcation`.
8. Caminho principal ate Lucarelli com inimigos.
9. Arena placeholder de Lucarelli.
10. Rota pos-Dash sobre a bifurcacao e a arena.
11. Marcador e parede de `Fim_Demo`.

O mapa inclui piso de seguranca sob as quedas abertas e marcadores `FutureDeathZone_*`. Eles reservam os espacos de queda para um futuro sistema de DeathZone sem implementa-lo agora.

## Apertar Play e testar o fluxo da demo
1. Rode `Tools > Tester > Build Bosque Demo Scene`.
2. Pressione Play com o Dash ainda bloqueado.
3. Ande com `A`/`D` ou setas, pule com `Space` e siga as placas placeholder.
4. Ataque os inimigos iniciais com `J` e confira dano no HUD.
5. Atravesse `Checkpoint_01_AfterCombat`.
6. Cruze o parkour e ative `Checkpoint_02_AfterParkour`.
7. Suba ate `DashGate_Bifurcation` para ver que a rota secundaria ainda esta bloqueada.
8. Siga o piso principal ate a arena e lute contra Lucarelli.
9. Ao derrota-lo, confira o feedback de Dash desbloqueado no HUD ou Console.
10. Use `Left Shift` para testar o Dash.
11. Volte a bifurcacao, atravesse a rota do gate e siga ate `Fim_Demo`.
12. Pressione `ESC` para testar abrir e fechar o menu de pausa.

Para testar respawn durante a demo, deixe um inimigo causar dano ou use o menu de contexto de `PlayerHealth` depois de ativar um checkpoint.

## Campos ligados automaticamente
O builder liga no Inspector:
- `GroundCheck`, `Ground Layer` e `AbilityManager` do `PlayerController2D`;
- `AttackPoint` e `Enemy Layers` do `PlayerCombat`;
- target da `CameraFollow2D`;
- textos, `PlayerHealth` e `AbilityManager` do `HUDController`;
- painel do `PauseMenuController`;
- botoes do painel de pausa;
- `RespawnPoint` dos checkpoints;
- collider, visual e `AbilityManager` dos Dash gates;
- alvo e recompensa de Dash de Lucarelli.

## Ajustes manuais possiveis
As cenas devem funcionar ao apertar Play depois do builder. Ainda pode valer ajustar manualmente:
- raio e posicao de `GroundCheck` se o placeholder de Rubens mudar;
- posicao de `AttackPoint` e raio da katana para outro tamanho de sprite;
- velocidade, vida, dano e patrulha dos inimigos;
- vida, intervalos e raios dos ataques de Lucarelli;
- distancia e altura de plataformas depois de testar o pulo real de Rubens;
- enquadramento e suavizacao da camera para a largura da demo;
- layout visual e tamanho dos textos do HUD, pausa e placas;
- ordem das cenas em Build Settings se o projeto adotar outro fluxo de build.

## Limitacoes da montagem automatica
- A cena demo e um blockout jogavel, nao um mapa final do Bosque.
- A duracao de 15 a 20 minutos ainda depende de playtest e ajuste de ritmo.
- Os marcadores `FutureDeathZone_*` nao matam nem respawnam Rubens.
- O piso de seguranca evita queda fora do mundo enquanto DeathZone nao existe.
- As placas usam texto placeholder e nao substituem tutorial final.
- Lucarelli continua com IA inicial sem telegraph visual final e sem segunda fase.
- Inimigos patrulham por distancia e nao detectam beirada.
- Checkpoint e Dash nao usam save persistente.
- HUD, pausa, colisores e sprites usam recursos simples de teste.
