# DEMO_MAP_PLAN

## Objetivo
Planejar uma demo fechada jogavel de `Tester` no Bosque da Nevoa Perdida com aproximadamente 15 a 20 minutos de conteudo usando placeholders.

A demo deve ter um fluxo completo:
- entrada clara;
- aprendizado gradual de movimento, pulo e katana;
- checkpoint e respawn testaveis;
- pequena leitura de exploracao metroidvania com bloqueio por Dash;
- luta contra Lucarelli;
- recompensa do Dash;
- trecho curto pos-Dash;
- fim da demo.

O plano usa apenas os sistemas atuais do prototipo. Arte final, save completo, novos biomas e novas habilidades ficam fora desta etapa.

## Escopo da demo
A demo deve parecer um recorte pequeno do Bosque, nao uma fase extensa do jogo completo.

Use uma cena expandida ou um conjunto curto de salas contiguas montadas com placeholders:
- pisos retangulares;
- plataformas simples;
- paredes solidas;
- nevoa e arvores apenas como marcadores visuais simples, se existirem;
- placas de tutorial curtas;
- portoes e limites claros.

A primeira versao deve priorizar ritmo e clareza para beta fechado. A pergunta principal do teste e: o jogador entende o fluxo basico de Rubens, vence Lucarelli e percebe por que o Dash importa?

## Estrutura geral
### Fluxo macro
```text
[Entrada do Bosque]
        |
        v
[Tutorial de Movimento]
        |
        v
[Combate Basico]
        |
        v
[Checkpoint 01]
        |
        v
[Parkour Basico]
        |
        v
[Bifurcacao]
   |             |
   |             +--> [Caminho bloqueado por Dash]
   v
[Caminho ate Lucarelli]
        |
        v
[Checkpoint 02 / Entrada da Arena]
        |
        v
[Arena do Lucarelli]
        |
        v
[Recompensa do Dash]
        |
        v
[Area Pos-Dash]
        |
        v
[Fim da Demo]
```

### Trechos planejados
| Trecho | Funcao principal | Conteudo minimo |
| --- | --- | --- |
| Entrada do Bosque | Dar contexto e spawn seguro | Piso largo, parede traseira, placa inicial, nenhum inimigo imediato |
| Area tutorial | Ensinar andar e pular | Saltos baixos, piso sem risco fatal, placa de movimento |
| Area de combate basico | Ensinar katana e dano | 2 inimigos com espaco para recuo, placa curta de ataque |
| Checkpoint 01 | Ensinar recuperacao | Checkpoint visivel depois do primeiro combate |
| Parkour basico | Testar salto e leitura de plataforma | Plataformas justas, 1 ou 2 quedas com kill zone e respawn |
| Bifurcacao | Mostrar promessa metroidvania | Caminho principal para o chefe e caminho curto bloqueado por Dash |
| Caminho ate Lucarelli | Preparar tensao antes do chefe | 2 a 3 inimigos, arena anunciada por corredor mais calmo |
| Arena de Lucarelli | Fechar o primeiro arco | Arena segura, Lucarelli, limites laterais e checkpoint proximo |
| Recompensa do Dash | Dar feedback de progressao | Unlock do Dash e espaco seguro para testar a habilidade |
| Area pos-Dash | Exigir a habilidade recem-ganha | Gate ou passagem com buraco curto que pede Dash |
| Fim da demo | Encerrar com clareza | Marcador de fim, parede/porta fechada e mensagem simples |

## Tempo estimado
### Orcamento de duracao
| Trecho | Duracao alvo | Observacao |
| --- | --- | --- |
| Entrada do Bosque | 1:00 | Jogador se orienta e le a primeira placa |
| Tutorial de movimento | 1:45 | Sem exigir precisao alta |
| Combate basico | 2:15 | Inclui primeiro contato com dano |
| Checkpoint 01 e transicao | 0:45 | Checkpoint deve ser impossivel de ignorar |
| Parkour basico | 2:00 | Pequeno desafio de plataforma com retorno rapido |
| Bifurcacao e leitura do gate | 0:45 | Jogador percebe que falta Dash |
| Caminho ate Lucarelli | 1:45 | Combate um pouco mais denso antes do chefe |
| Checkpoint 02 e entrada da arena | 0:30 | Preparacao curta antes da luta |
| Lucarelli | 3:30 | Inclui leitura dos dois ataques e uma derrota ocasional |
| Recompensa e teste livre do Dash | 0:45 | Espaco seguro logo apos a luta |
| Area pos-Dash | 1:45 | Uma ou duas exigencias simples de Dash |
| Fim da demo | 0:20 | Encerramento objetivo |

Duracao alvo da primeira montagem: aproximadamente `17` minutos.

Com leitura de placas, hesitacao em saltos, uma queda normal ou uma tentativa extra em Lucarelli, a mesma estrutura deve ficar perto de `15` a `20` minutos para amigos que nao conhecem a cena.

Para manter o alvo de 15 a 20 minutos:
1. Teste primeiro com um jogador que nao conhece a cena.
2. Se a run passar de 20 minutos sem morrer no chefe, encurte o caminho ate Lucarelli ou reduza o parkour.
3. Se a run ficar abaixo de 15 minutos, aumente leitura e variedade no caminho principal antes de crescer a arena.
4. Nao inflar duracao com corredores vazios; cada trecho deve ensinar, testar ou recompensar algo.

### Duracao recomendada para a primeira montagem
Use como referencia inicial:
- pre-Lucarelli: `9` a `11` minutos;
- luta e recompensa: `4` a `5` minutos;
- pos-Dash e fim: `2` a `3` minutos.

Isso deixa a demo por volta de `15` a `19` minutos quando o jogador entende o basico, com margem para erros normais.

## Fluxo do jogador
### 1. Entrada do Bosque
O jogador aprende:
- que Rubens comeca em um trecho seguro;
- que a leitura segue da esquerda para a direita na primeira versao;
- onde esta o limite inicial da demo.

Montagem:
- spawn sobre piso largo;
- parede invisivel ou parede placeholder atras do spawn;
- placa `Mover: A/D ou setas`;
- placa `Pular: Space` antes do primeiro salto.

Inimigos:
- nenhum inimigo encostado no spawn;
- opcionalmente um inimigo distante visivel adiante para antecipar combate.

### 2. Area tutorial
O jogador aprende:
- andar;
- pular;
- pousar em plataformas baixas;
- reconhecer piso seguro e buraco curto.

Montagem:
- um salto horizontal simples;
- uma plataforma um pouco mais alta;
- retorno seguro se o jogador cair na primeira versao;
- kill zone apenas depois que o jogador ja viu o checkpoint ou em queda muito clara.

### 3. Area de combate basico
O jogador aprende:
- atacar com a katana;
- observar dano por contato;
- recuar e reposicionar antes de atacar de novo.

Montagem:
- arena pequena aberta no caminho;
- placa `Katana: J`;
- `Enemy_01` sozinho;
- `Enemy_02` depois de um pequeno espaco, sem sobrepor o primeiro inimigo.

Evite:
- encurralar Rubens entre dois inimigos na primeira sala de combate;
- colocar buraco perigoso dentro da area em que o jogador ainda esta aprendendo a atacar.

### 4. Checkpoint 01
O jogador aprende:
- que checkpoints marcam progresso;
- que morrer nao reinicia a demo inteira.

Montagem:
- checkpoint no caminho principal, visivel depois do combate inicial;
- respawn point sobre piso seguro;
- placa curta `Checkpoint` se o feedback do HUD ainda for discreto para amigos.

### 5. Parkour basico
O jogador aprende:
- combinar salto horizontal e altura;
- ler plataforma antes de entrar em combate;
- aceitar uma queda pequena sem perder muito tempo.

Montagem:
- 3 a 5 plataformas principais;
- pelo menos uma plataforma larga de descanso;
- um buraco com kill zone somente quando a queda estiver claramente sinalizada;
- uma parede lateral ou piso de retorno para nao cair fora do mundo.

Inimigos:
- nenhum inimigo em plataforma muito estreita;
- opcionalmente 1 inimigo em plataforma larga depois do trecho de salto.

### 6. Bifurcacao com bloqueio por Dash
O jogador aprende:
- que existe um caminho que ainda nao pode acessar;
- que o caminho principal continua sem Dash;
- que a habilidade futura tem uso no mapa.

Montagem:
- bifurcacao visivel e curta;
- caminho secundario com `AbilityGate` exigindo Dash;
- placa placeholder ou mensagem de gate indicando falta do Dash;
- caminho principal apontando para Lucarelli.

Recomendacao:
- deixe o gate perto o bastante do caminho principal para o jogador lembrar dele depois;
- nao use a bifurcacao para esconder o caminho ao chefe.

### 7. Caminho ate Lucarelli
O jogador aprende:
- aplicar movimento e katana em uma sequencia um pouco mais longa;
- reconhecer a aproximacao de um chefe por layout mais controlado.

Montagem:
- 2 ou 3 encontros curtos com inimigos basicos;
- 1 plataforma ou desvio simples, nao outro parkour longo;
- corredor final mais calmo antes da arena;
- `Checkpoint 02` antes da luta.

### 8. Arena do Lucarelli
O jogador aprende:
- ler o avanco horizontal;
- respeitar o ataque curto em area;
- atacar em janelas seguras.

Montagem:
- piso principal largo;
- limites laterais claros;
- teto aberto ou alto o bastante para nao brigar com o pulo;
- sem buraco fatal dentro da primeira versao da arena;
- checkpoint imediatamente antes da entrada.

Lucarelli aparece:
- no centro ou no terco oposto da arena;
- com espaco para Rubens entrar e ganhar controle antes do primeiro ataque.

### 9. Recompensa do Dash
O jogador aprende:
- que derrotar Lucarelli desbloqueou progressao;
- qual tecla usa o Dash;
- que a habilidade serve para atravessar espaco e gate.

Montagem:
- espaco seguro depois da luta ou na saida da arena;
- placa curta `Dash: Left Shift` se necessario;
- trecho reto para testar Dash sem perigo imediato.

### 10. Area pos-Dash
O jogador aprende:
- usar Dash para acessar o caminho antes bloqueado;
- usar Dash em uma travessia simples de mapa.

Montagem:
- retorno curto ao gate visto na bifurcacao ou uma saida da arena que reconecta nesse ponto;
- primeira exigencia: atravessar gate que abre com Dash desbloqueado;
- segunda exigencia opcional: buraco horizontal maior que o salto comum, mas facil com Dash;
- checkpoint extra apenas se a queda pos-Dash estiver causando repeticao ruim no teste.

### 11. Fim da demo
O jogador entende:
- que concluiu o recorte do Bosque;
- que a proxima etapa existe fora desta demo.

Montagem:
- marcador `Fim_Demo`;
- parede, porta ou limite visual que impede continuar;
- texto placeholder curto, por exemplo `Fim da demo do Bosque`.

## Regras de design
### Seguranca e respawn
- Nao permitir queda sem destino ou respawn.
- Fechar bordas externas com paredes solidas, paredes invisiveis ou kill zones controladas.
- Toda kill zone deve usar um `DeathZone` trigger ou comportamento equivalente que leve a um respawn previsivel pelo checkpoint atual.
- Posicionar respawn points sobre piso largo, fora de inimigos, gate e buracos.
- Evitar morte imediata depois do respawn.

### Legibilidade
- Usar paredes invisiveis apenas quando o limite visual do placeholder ainda nao estiver claro.
- Dar silhueta simples para pisos, plataformas, gates e portas mesmo com quadrados.
- Sinalizar bifurcacao, checkpoint, arena e fim da demo com objetos maiores ou placas.
- Nao esconder o caminho principal atras de gate ou salto ambiguo.

### Plataforma
- Colocar plataformas com saltos justos para o pulo atual de Rubens.
- Introduzir altura e distancia separadamente antes de combinar as duas.
- Manter area de pouso maior antes e depois de buracos com kill zone.
- Fazer parkour basico; a demo fechada nao deve exigir precisao exagerada.

### Ritmo
- Alternar aprendizado, combate, descanso e desafio de plataforma.
- Nao criar mapa grande demais para aumentar duracao artificialmente.
- Usar poucos encontros bem posicionados antes de Lucarelli.
- Reservar o maior pico de dificuldade para o chefe.
- Fazer o trecho pos-Dash curto, claro e recompensador.

## Lista de objetos necessarios
### Objetos de layout
| Tipo | Uso na demo | Quantidade inicial sugerida |
| --- | --- | --- |
| Plataformas de piso | Caminho principal e arena | 10 a 16 |
| Plataformas elevadas | Tutorial e parkour | 5 a 8 |
| Paredes solidas | Limites de salas e arena | 6 a 10 |
| Paredes invisiveis | Limites externos temporarios | conforme bordas abertas |
| Buracos | Tutorial leve, parkour e pos-Dash | 3 a 5 |
| `DeathZone` triggers | Buracos e fundo do mapa | 3 a 5 |

### Objetos de gameplay
| Tipo | Uso na demo | Quantidade inicial sugerida |
| --- | --- | --- |
| Rubens_Player | Spawn e controle do jogador | 1 |
| Checkpoints | Antes/depois de trechos de risco | 2 obrigatorios, 1 opcional |
| Inimigos basicos | Combate inicial e caminho ao chefe | 5 a 8 |
| Gate por Dash | Promessa e progressao | 1 obrigatorio |
| Lucarelli | Fecho do arco da demo | 1 |
| Arena | Piso e limites do chefe | 1 |
| Placas de tutorial | Movimento, pulo, katana e Dash | 3 a 5 |
| Fim da demo | Encerramento claro | 1 |

### Objetos de suporte
- `GameManager`.
- `Main Camera` com `CameraFollow2D`.
- Canvas com HUD simples.
- Menu de pausa.
- `DeathZone` com `Collider2D` trigger sob buracos e uma cobertura de fundo do mapa.
- Respawn points seguros em cada checkpoint.
- Marcadores de area na Hierarchy para organizar entrada, tutorial, combate, parkour, bifurcacao, arena e pos-Dash.

## Proposta de ordem de construcao
1. Bloquear o mapa inteiro com pisos, paredes, buracos e fim da demo.
2. Garantir que toda queda perigosa tenha kill zone e respawn valido.
3. Colocar Rubens, camera, HUD e pausa.
4. Montar tutorial de movimento e combate.
5. Colocar checkpoints.
6. Montar bifurcacao e Dash gate.
7. Montar caminho ate Lucarelli e arena.
8. Conectar recompensa do Dash ao trecho pos-Dash.
9. Fazer uma run sem inimigos para validar leitura e duracao de deslocamento.
10. Fazer uma run completa com inimigos e Lucarelli para ajustar o alvo de 15 a 20 minutos.

## Limites deste plano
- Nao define arte final, audio final ou narrativa completa.
- Nao pede Kin, Shico, Templo dos Ventos ou Corte de Vento.
- Nao cria inventario, save completo ou novos sistemas grandes.
- Nao transforma o Bosque em mapa aberto amplo; a demo e um recorte fechado e legivel.
