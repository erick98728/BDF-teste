# DEMO_MAP_PLAN

## Objetivo
Planejar uma demo fechada jogavel de `Tester` no Bosque da Nevoa Perdida com aproximadamente 20 a 30 minutos de conteudo usando placeholders.

A demo deve ter um fluxo completo:
- entrada clara;
- aprendizado gradual de movimento, pulo e katana;
- hub central com rotas conectadas;
- retorno produtivo a um gate visto antes;
- checkpoint e respawn testaveis;
- leitura simples de exploracao metroidvania com bloqueio por Dash;
- luta contra Lucarelli;
- recompensa do Dash;
- trecho curto pos-Dash;
- fim da demo.

O plano usa apenas os sistemas atuais do prototipo. Arte final, save completo, novos biomas e novas habilidades ficam fora desta etapa.

## Escopo da demo
A demo deve parecer um recorte conectado do Bosque, nao uma fase extensa do jogo completo.

Use uma cena expandida ou um conjunto curto de salas contiguas montadas com placeholders:
- pisos retangulares;
- plataformas simples;
- paredes solidas;
- nevoa e arvores apenas como marcadores visuais simples, se existirem;
- placas de tutorial curtas;
- placas trigger com mensagens curtas no HUD;
- portoes e limites claros.

A primeira versao deve priorizar ritmo e clareza para beta fechado. A pergunta principal do teste e: o jogador entende o Hub Central, vence Lucarelli e percebe por que o Dash muda o caminho de volta?

## Estrutura geral
### Fluxo macro
```text
[Entrada do Bosque]
        |
        v
[Clareira Tutorial]
        |
        v
[Hub Central]
   |            |                 |
   v            v                 v
[Combate]   [Raizes]          [Copas]
   |            |                 |
   +------------+-----------------+
                |
                v
[Caminho ate Lucarelli]
        |
        v
[Checkpoint da Arena]
        |
        v
[Arena do Lucarelli]
        |
        v
[Recompensa do Dash]
        |
        v
[Retorno ao Hub / Gate do Dash]
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
| Hub Central | Orientar e deixar o gate memoravel | Arvore ou clarão placeholder, quatro saidas e `DashGate_Principal_Hub` visivel, sem combate obrigatório |
| Caminho de combate | Ensinar katana e dano | 3 inimigos no maximo, sempre com espaco para recuo |
| Raizes inferiores | Testar risco controlado | Pisos largos, tuneis baixos, 2 buracos principais com `DeathZone`, nevoa densa e retorno fisico ao hub |
| Copas superiores | Testar verticalidade | Subida gradual sem Dash, mirante alto, troncos de fundo e rota alternativa curta de volta ao hub |
| Caminho ate Lucarelli | Preparar tensao antes do chefe | Poucos inimigos e checkpoint antes da arena |
| Arena de Lucarelli | Fechar o primeiro arco | Arena segura, Lucarelli, limites laterais e checkpoint proximo |
| Recompensa do Dash | Dar feedback de progressao | Unlock do Dash e espaco seguro para testar a habilidade |
| Retorno ao gate | Recontextualizar o hub | Atalho alto pos-Lucarelli ate o `DashGate_Principal_Hub` visto antes |
| Area pos-Dash | Exigir a habilidade recem-ganha | Gate e 2 ou 3 gaps de `5` a `8` unidades com pousos seguros |
| Fim da demo | Encerrar com clareza | Marcador de fim, parede/porta fechada e mensagem simples |

## Tempo estimado
### Orcamento de duracao
| Trecho | Duracao alvo | Observacao |
| --- | --- | --- |
| Entrada e clareira tutorial | 2:00 a 3:00 | Pouco risco ate o hub |
| Leitura do Hub Central | 1:00 a 1:30 | Gate e rotas precisam ser reconheciveis |
| Caminho de combate | 3:00 a 4:30 | Inclui treino de katana |
| Raizes inferiores | 2:30 a 4:00 | Quedas curtas voltam por checkpoint |
| Copas superiores | 2:30 a 4:00 | Verticalidade sem Dash |
| Caminho ate Lucarelli | 2:30 a 4:00 | Convergencia e preparacao |
| Lucarelli | 4:00 a 6:00 | Inclui leitura dos ataques e erro normal |
| Retorno ao gate e pos-Dash | 3:00 a 4:30 | Recompensa e uso da habilidade |
| Fim da demo | 0:30 a 1:00 | Encerramento objetivo |

Duracao alvo da montagem metroidvania: aproximadamente `20` a `30` minutos.

Com leitura de placas, uma rota opcional, uma queda normal ou uma tentativa extra em Lucarelli, a estrutura deve ficar nessa faixa para amigos que nao conhecem a cena.

Para manter o alvo de 20 a 30 minutos:
1. Teste primeiro com um jogador que nao conhece a cena.
2. Se a run passar de 30 minutos sem morrer no chefe, reduza repeticao entre hub, convergencia e arena.
3. Se a run ficar abaixo de 20 minutos, aumente leitura e variedade das rotas antes de crescer corredores.
4. Nao inflar duracao com corredores vazios; cada trecho deve ensinar, testar ou recompensar algo.

### Duracao recomendada para a primeira montagem
Use como referencia inicial:
- pre-Lucarelli: `13` a `18` minutos;
- luta e recompensa: `4` a `5` minutos;
- pos-Dash e fim: `3` a `5` minutos.

Isso deixa a demo por volta de `20` a `28` minutos quando o jogador entende o basico, com margem para erros normais.

### Distribuicao atual de ritmo
| Area | Inimigos basicos | Checkpoint | Area segura | Risco principal |
| --- | --- | --- | --- | --- |
| Entrada do Bosque | `0` | Nao | Spawn e tutorial inicial | Nenhum risco fatal |
| Hub Central | `0` | Nao | Area aberta de orientacao | Leitura de rotas e DashGate |
| Combate inicial | `3` | Depois do trecho | Espaco entre encontros | Dano por contato |
| Caminho Inferior das Raizes | `2` | Nao | Plataformas largas antes/depois dos buracos | 2 buracos com `DeathZone` |
| Caminho Superior das Copas | `1` | Nao | Plataformas de descanso e rota de retorno | Quedas de parkour vertical |
| Convergencia | `0` | `Checkpoint_02_Convergence` | Ponto de respiro antes do pre-chefe | Nenhum risco imediato |
| Caminho pre-Lucarelli | `2` | Nao | Corredor final calmo antes da arena | Dois encontros separados |
| Entrada da arena | `0` | `Checkpoint_03_ArenaEntry` | Retentativa rapida do chefe | Nenhum inimigo comum |
| Arena de Lucarelli | `0` comuns + `Lucarelli` | Nao | Chao amplo e paredes laterais | Chefe |
| Retorno pos-Lucarelli | `0` | Nao | Atalho alto ate o gate | Orientacao de retorno |
| Area pos-Dash | `0` | Nao | Pousos largos e descanso entre gaps | Vaos de Dash |

Essa distribuicao evita inimigos no spawn, no Hub Central, em cima de checkpoints e no retorno pos-chefe. O pico de pressao fica em Lucarelli, com respiro imediatamente antes e depois da luta.

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
- `Enemy_03` deve ficar depois de outro respiro curto, ainda em piso largo e sem buraco por perto.

Evite:
- encurralar Rubens entre dois inimigos na primeira sala de combate;
- colocar buraco perigoso dentro da area em que o jogador ainda esta aprendendo a atacar.

### 4. Checkpoint 01
O jogador aprende:
- que checkpoints marcam progresso;
- que morrer nao reinicia a demo inteira.

Montagem:
- `Checkpoint_01_AfterCombat` no caminho principal, visivel depois do combate inicial;
- respawn point sobre piso seguro;
- pelo menos `6` unidades de distancia do inimigo mais proximo;
- placa curta `Checkpoint` se o feedback do HUD ainda for discreto para amigos.

### 5. Parkour basico
O jogador aprende:
- combinar salto horizontal e altura;
- ler plataforma antes de entrar em combate;
- aceitar uma queda pequena sem perder muito tempo.

Montagem:
- 3 a 5 plataformas principais;
- saltos horizontais entre `2.5` e `4` unidades antes do Dash;
- diferencas de altura entre `1` e `2.5` unidades nas subidas obrigatorias;
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
- caminho secundario com `DashGate_Principal_Hub` exigindo Dash;
- placa placeholder indicando que falta uma tecnica;
- mensagem do gate: `Você ainda não domina a técnica necessária para atravessar.`;
- caminho principal apontando para Lucarelli.

Recomendacao:
- deixe o gate perto o bastante do caminho principal para o jogador lembrar dele depois;
- nao use a bifurcacao para esconder o caminho ao chefe.
- o gate deve bloquear apenas a area pos-Dash, nunca a rota sem Dash ate Lucarelli.

Atalhos atuais do blockout:
- `Canopy_AltRoute_*` permite abandonar a rota das copas e voltar ao Hub Central sem morte;
- `Roots_ReturnToHub_A-D` sobe das raizes para o Hub Central por plataformas largas;
- `Shortcut_ArenaToGate_*` encurta o retorno de Lucarelli ate o gate depois que o Dash e liberado.

### 7. Caminho ate Lucarelli
O jogador aprende:
- aplicar movimento e katana em uma sequencia um pouco mais longa;
- reconhecer a aproximacao de um chefe por layout mais controlado.

Montagem:
- 2 encontros curtos com inimigos basicos, separados por espaco suficiente para reposicionar;
- 1 plataforma ou desvio simples, nao outro parkour longo;
- corredor final mais calmo antes da arena;
- `Checkpoint_03_ArenaEntry` antes da luta.

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
- feedback do HUD: `Dash desbloqueado. Use Shift esquerdo para avançar rapidamente.`;
- placa curta `Dash: Shift esquerdo` se necessario;
- trecho reto para testar Dash sem perigo imediato.

### 10. Area pos-Dash
O jogador aprende:
- usar Dash para acessar o caminho antes bloqueado;
- usar Dash em uma travessia simples de mapa.

Montagem:
- retorno curto ao `DashGate_Principal_Hub` por `Shortcut_ArenaToGate_*`;
- primeira exigencia: atravessar gate que abre com Dash desbloqueado;
- segunda exigencia: vaos horizontais de `5` a `8` unidades, maiores que o salto comum e confortaveis com Dash;
- dica da area: `Use Shift esquerdo para cruzar vaos maiores.`;
- plataformas de pouso largas entre os vaos para o jogador recuperar controle;
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
- Usar limites de camera para evitar mostrar area vazia demais fora do blockout expandido.
- Separar as regioes por cor e densidade de nevoa, mantendo contraste suficiente entre fundo, plataformas, inimigos e Lucarelli.
- Manter decoracao sem `Collider2D` e atras do gameplay para ela nunca ser confundida com caminho jogavel.

### Ambientacao placeholder
O builder da demo usa apenas retangulos com `SpriteRenderer` para criar clima:
- Entrada do Bosque: verde mais claro, linha de arvores e pouca nevoa.
- Hub Central: azul esverdeado, arvore grande, pedra luminosa e luzes indicando rotas.
- Raizes inferiores: azul/roxo escuro, raizes grandes e fog mais denso perto dos buracos.
- Copas superiores: verde frio, troncos altos, copa no fundo e nevoa leve.
- Caminho para Lucarelli: roxo pesado, fundo corrompido e pouca luz.
- Arena de Lucarelli: fundo dramatico em vermelho, roxo e dourado escuro para destacar o chefe.
- Area pos-Dash: ciano mais limpo, menos nevoa pesada e luzes de recompensa.

Essa ambientacao serve para leitura de blockout. Ela nao define arte final, parallax, particulas ou iluminacao 2D.

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

### Objetos visuais
| Tipo | Uso na demo | Observacao |
| --- | --- | --- |
| Silhuetas de fundo | Arvores, copas, raizes e arena | Sem collider, atras do gameplay |
| Nevoa placeholder | Raizes, pre-Lucarelli, arena e buracos | Opacidade baixa para nao atrapalhar leitura |
| Pontos de luz | Caminhos, checkpoints, DashGate e fim | Marcadores simples, sem luz real |
| Landmarks | Hub, DashGate, arena e fim da demo | Formas grandes e memoraveis |
| Marcadores de regiao | Separar paleta de cada area | Retangulos translucidos no fundo |

### Objetos de gameplay
| Tipo | Uso na demo | Quantidade inicial sugerida |
| --- | --- | --- |
| Rubens_Player | Spawn e controle do jogador | 1 |
| Checkpoints | Depois do primeiro combate, convergencia e entrada da arena | 3 obrigatorios |
| Inimigos basicos | Combate inicial, raizes, copas e caminho ao chefe | 8 |
| Gate por Dash | Promessa e progressao | 1 obrigatorio |
| Lucarelli | Fecho do arco da demo | 1 |
| Arena | Piso e limites do chefe | 1 |
| Placas de tutorial | Movimento, pulo, katana, checkpoint, rotas, Dash e fim | 10 a 12 |
| Fim da demo | Encerramento claro | 1 |

### Objetos de sinalizacao
| Area | Sinal esperado | Mensagem |
| --- | --- | --- |
| Entrada | Movimento e pulo | `Use A/D ou as setas para se mover. Espaço para pular.` |
| Primeiro combate | Katana | `Pressione J para atacar com a katana.` |
| Checkpoint | Respawn | `Checkpoints definem seu ponto de retorno após uma queda ou derrota.` |
| Primeiro buraco | Queda | `Cuidado com os buracos. Se cair, você volta ao último checkpoint.` |
| Hub | Exploracao | `O Bosque se divide em vários caminhos. Observe os marcos e siga com atenção.` |
| Copas | Parkour | `As copas exigem precisão nos pulos. Suba com calma.` |
| Raizes | Risco | `As raízes escondem quedas e passagens. Avance com cuidado.` |
| DashGate | Bloqueio | `Uma força bloqueia o caminho. Talvez uma nova técnica permita atravessar.` |
| Lucarelli | Chefe | `Lucarelli guarda a passagem. Prepare-se antes de avançar.` |
| Pos-Dash | Dash | `Use Shift esquerdo para cruzar vãos maiores.` |
| Fim | Encerramento | `Fim da demo. Obrigado por jogar esta versão de teste.` |

### Objetos de suporte
- `GameManager`.
- `Main Camera` com `CameraFollow2D`.
- `CameraFollow2D` com `Use Bounds` ligado na demo expandida:
  - `Min X = -248`;
  - `Max X = 108`;
  - `Min Y = -4.5`;
  - `Max Y = 9.5`.
- Canvas com HUD simples.
- Menu de pausa.
- `DeathZone` com `Collider2D` trigger sob buracos e uma cobertura de fundo do mapa.
- Respawn points seguros em cada checkpoint.
- Marcadores de area na Hierarchy para organizar entrada, tutorial, combate, parkour, bifurcacao, arena e pos-Dash.
- `Decoration/Background`, `Decoration/Fog`, `Decoration/Lights`, `Decoration/Landmarks` e `Decoration/RegionMarkers` para organizar a ambientacao sem colisao.
- `Tutorial/Signs` com `TutorialSign`, collider trigger e mensagem curta para o HUD.

## Proposta de ordem de construcao
1. Bloquear o mapa inteiro com pisos, paredes, buracos e fim da demo.
2. Garantir que toda queda perigosa tenha kill zone e respawn valido.
3. Colocar Rubens, camera, HUD e pausa.
4. Montar tutorial de movimento e combate.
5. Colocar checkpoints.
6. Montar bifurcacao e Dash gate.
7. Montar caminho ate Lucarelli e arena.
8. Conectar recompensa do Dash ao trecho pos-Dash.
9. Validar os limites da camera nas bordas esquerda, direita, alta e baixa do blockout.
10. Aplicar decoracao placeholder por regiao sem adicionar colisores.
11. Colocar sinais trigger nos pontos de ensino sem bloquear movimento.
12. Fazer uma run sem inimigos para validar leitura, contraste, mensagens e duracao de deslocamento.
13. Fazer uma run completa com inimigos e Lucarelli para ajustar o alvo de 20 a 30 minutos.

## Limites deste plano
- Nao define arte final, audio final ou narrativa completa.
- Nao pede Kin, Shico, Templo dos Ventos ou Corte de Vento.
- Nao cria inventario, save completo ou novos sistemas grandes.
- Nao transforma o Bosque em mapa aberto amplo; a demo e um recorte fechado e legivel.
- A ambientacao atual ainda e placeholder: cores, retangulos, silhuetas e nevoa simples.
