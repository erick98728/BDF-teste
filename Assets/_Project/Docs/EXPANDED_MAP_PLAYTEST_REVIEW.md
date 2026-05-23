# EXPANDED_MAP_PLAYTEST_REVIEW

## Objetivo
Registrar uma revisao tecnica de playtest da `Prototype_Bosque_Demo` expandida, sem ampliar escopo nem criar sistemas grandes novos.

Esta revisao cobre spawn, movimentacao, seguranca do mapa, progressao por Dash, checkpoints, combate, HUD, tutorial, pausa, camera, visual placeholder e documentacao.

## Resumo do estado atual
A demo expandida esta montada pelo `PrototypeSceneBuilder` como um recorte fechado do Bosque da Nevoa Perdida.

O fluxo principal ja inclui:
- spawn seguro de Rubens;
- entrada e tutorial inicial;
- Hub Central com rotas conectadas;
- combate inicial;
- caminho inferior das raizes;
- caminho superior das copas;
- `DashGate_Principal_Hub` visto antes do Dash;
- caminho sem Dash ate Lucarelli;
- checkpoint antes da arena;
- arena de Lucarelli sem inimigos comuns;
- recompensa do Dash ao derrotar Lucarelli;
- retorno curto ao DashGate;
- area pos-Dash;
- marcador claro de fim da demo.

O mapa continua usando placeholders simples. A revisao nao encontrou necessidade de novo sistema de gameplay.

## Areas da Prototype_Bosque_Demo
| Area | Funcao | Estado |
| --- | --- | --- |
| `Entrance` | Spawn, movimento e primeiro pulo | Segura, sem inimigos |
| `CentralHub` | Orientacao e leitura de rotas | Conecta combate, raizes, copas e DashGate |
| `CombatPath` | Primeiro treino de katana | 3 inimigos basicos separados |
| `LowerRoots` | Rota baixa com risco controlado | 2 inimigos, 2 buracos principais e `DeathZone` |
| `UpperCanopy` | Verticalidade antes do Dash | 1 inimigo em plataforma larga e rota alternativa |
| `LucarelliPath` | Preparacao de chefe | 2 inimigos separados e sinalizacao |
| `LucarelliArena` | Luta principal da demo | Chao seguro, paredes laterais e Lucarelli destacado |
| `DashReturnGate` | Atalho pos-Lucarelli | Retorno fisico ao DashGate, sem teletransporte |
| `PostDashArea` | Teste curto do Dash | Vaos maiores, pousos seguros e DeathZones |
| `DemoEnd` | Encerramento | Marcador visual, limite fisico e mensagem final |

## Fluxo completo do jogador
1. Rubens nasce na entrada, sobre piso seguro.
2. O jogador aprende movimento e pulo com placas curtas.
3. O Hub Central apresenta rotas e o marco principal do mapa.
4. O combate inicial ensina katana sem inimigos colados.
5. `Checkpoint_01_AfterCombat` salva progresso em piso seguro.
6. O jogador explora raizes ou copas e encontra retornos para o hub ou para a convergencia.
7. `Checkpoint_02_Convergence` cria respiro antes do caminho pre-chefe.
8. O jogador ve o `DashGate_Principal_Hub` antes de ter Dash.
9. A rota ate Lucarelli permanece acessivel sem Dash.
10. `Checkpoint_03_ArenaEntry` permite tentar o chefe novamente sem repetir muito caminho.
11. Lucarelli e derrotado e chama a recompensa do Dash.
12. O HUD mostra que o Dash foi desbloqueado.
13. O jogador volta pelo atalho alto ate o DashGate.
14. O gate abre com Dash desbloqueado.
15. A area pos-Dash testa vaos maiores e termina no fim da demo.

## Tempo estimado por area
| Trecho | Tempo alvo | Observacao |
| --- | --- | --- |
| Entrada e tutorial | 2 a 3 min | Aprendizado sem risco fatal |
| Leitura do Hub | 1 a 2 min | Jogador reconhece rotas e gate |
| Combate inicial | 3 a 4 min | Inclui primeira leitura da katana |
| Raizes inferiores | 3 a 4 min | Risco de queda com respawn |
| Copas superiores | 3 a 4 min | Parkour basico sem Dash |
| Convergencia e pre-Lucarelli | 3 a 5 min | Preparacao e checkpoint de arena |
| Lucarelli | 4 a 6 min | Inclui erro normal e nova tentativa |
| Retorno e pos-Dash | 3 a 5 min | Recompensa e teste da habilidade |
| Fim da demo | 0.5 a 1 min | Encerramento claro |

Duracao esperada: aproximadamente 20 a 30 minutos para jogador novo, considerando leitura, exploracao curta, erros normais e luta contra Lucarelli.

## Checklist de teste
| Area de teste | Resultado da revisao |
| --- | --- |
| Spawn | Rubens nasce em piso seguro, sem inimigos e sem DeathZone proxima |
| Camera | `Main Camera` segue Rubens e usa bounds configurados na demo |
| HUD | Canvas e `HUDController` sao criados pelo builder |
| Movimento | Saltos obrigatorios antes de Lucarelli nao exigem Dash |
| Dash | Vaos de Dash ficam na area pos-Dash, atras do gate |
| DeathZones | Raizes, gaps pos-Dash e fundo geral possuem triggers |
| Limites | Entrada, arena, direita do mapa e fim da demo possuem limites fisicos |
| Checkpoints | 3 checkpoints estao longe de inimigos imediatos |
| Combate | Inimigos basicos estao distribuidos por area, sem arena cheia |
| Lucarelli | Arena limpa, chefe visivel e recompensa de Dash conectada |
| Tutorial | `TutorialSign` cobre movimento, katana, checkpoint, buracos, hub, gate, chefe, Dash e fim |
| Pausa | `PauseMenuController` existe no Canvas e controla `Time.timeScale` |
| Visual | Decoracao fica sem collider e atras do gameplay |

## Bugs encontrados
1. `DashGate_Principal_Hub` podia ser alto apenas o suficiente para bloquear o caminho frontal, mas ainda havia risco de o jogador tentar pular por cima antes do Dash.
2. `Enemy_Canopy_01` estava posicionado baixo demais em relacao a plataforma das copas, com risco de nascer parcialmente encostado no piso.

## Bugs corrigidos
1. O collider do `DashGate_Principal_Hub` foi aumentado no builder para bloquear tambem a tentativa de pular por cima antes do Dash.
2. `Enemy_Canopy_01` foi reposicionado levemente para cima para nascer corretamente sobre a plataforma.
3. A documentacao do builder foi atualizada para registrar que o DashGate usa collider alto para impedir bypass antes do Dash.

## Bugs pendentes
- Nao foi feito playtest manual em tempo real com controle humano nesta revisao; a duracao de 20 a 30 minutos ainda precisa de medicao real.
- A camera usa bounds globais, nao camera por sala; algumas regioes podem precisar de ajuste fino apos captura ou playtest.
- As patrulhas dos inimigos ainda sao simples e nao detectam beirada; o blockout precisa continuar limitando cada patrulha.
- O retorno pos-Lucarelli e fisico, mas ainda depende de sinalizacao placeholder para ficar totalmente obvio a jogadores novos.
- O reset por `RestartScene` ainda depende do comportamento atual do `GameManager` persistente; validar no Editor antes de beta fechado.
- A demo nao possui prefabs finais, arte final, audio, save permanente nem telegraphs visuais completos.

## Riscos tecnicos
- Como a cena e gerada automaticamente, ajustes manuais diretos em `Prototype_Bosque_Demo.unity` podem ser sobrescritos ao rodar o builder.
- O tamanho do mapa exige testar camera, respawn e mensagens em uma run completa, nao apenas por regioes isoladas.
- Textos de tutorial podem competir com mensagens de dano, queda, respawn e Dash se o jogador atravessar muitos triggers rapido.
- Gaps pos-Dash devem ser testados com o Dash real em Play Mode para confirmar que sao desafiadores sem ficarem frustrantes.
- Lucarelli ainda usa comportamento de prototipo; sem telegraph visual, jogadores novos podem precisar de ajuste de arena ou tempo de ataque.

## Proximos passos antes do beta fechado
1. Fazer uma run completa gravando tempo por area e numero de mortes.
2. Testar os 3 checkpoints com morte por inimigo e queda em `DeathZone`.
3. Confirmar que o jogador ve o DashGate antes de Lucarelli e entende que deve voltar depois.
4. Testar Lucarelli com pelo menos uma pessoa que nao conhece os padroes.
5. Ajustar apenas espacamento, patrulha e sinalizacao se a demo ficar confusa.
6. Criar uma checklist curta para amigos reportarem: tempo total, onde morreram, onde se perderam e se entenderam o Dash.
7. Antes de chamar de beta fechado, fazer build Windows e validar pausa, quit, restart, HUD, checkpoint e fim da demo fora do Editor.
