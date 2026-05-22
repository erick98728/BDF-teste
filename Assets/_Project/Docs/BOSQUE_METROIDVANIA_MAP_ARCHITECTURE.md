# BOSQUE_METROIDVANIA_MAP_ARCHITECTURE

## Objetivo
Definir a arquitetura tecnica de uma demo expandida do Bosque da Nevoa Perdida com `20` a `30` minutos jogaveis, ainda montada com placeholders e os sistemas atuais do prototipo.

Esta versao deve evoluir a demo linear atual para um mapa pequeno e interconectado:
- a entrada continua clara para novos jogadores;
- o hub central organiza rotas principais sem virar labirinto;
- o Dash cria progressao por habilidade e retorno a area anterior;
- atalhos reduzem repeticao depois de explorar;
- Lucarelli fecha o arco pre-Dash;
- a area pos-Dash conclui a demo com um acesso novo e um fim claro.

O documento serve como referencia para um proximo blockout no `PrototypeSceneBuilder` e para ajustes manuais na Unity. Ele nao pede alteracao de cena neste passo.

## Escopo da nova demo
### Dentro do escopo
- Um recorte maior do `Bosque da Nevoa Perdida`.
- Movimento, pulo, katana, dano, checkpoint, respawn, Dash, gate de habilidade, HUD, pausa, Lucarelli e `DeathZone`.
- Exploracao com duas rotas simples que se reencontram.
- Backtracking curto e legivel depois do Dash.
- Placeholders com nomes e silhuetas claras.

### Fora do escopo
- Copiar layout, arte, nomes de salas ou ritmo de `Hollow Knight`.
- Criar novos biomas.
- Adicionar Kin, Shico, Corte de Vento, save completo, inventario ou narrativa final.
- Finalizar arte, efeitos, audio, cutscenes ou tutorial definitivo.
- Transformar o Bosque em um mapa amplo de jogo completo.

## Estrutura macro do mapa
### Regioes obrigatorias
| Regiao | Papel no fluxo | Leitura desejada |
| --- | --- | --- |
| Entrada do Bosque | Spawn seguro e orientacao inicial | Caminho principal evidente para a direita |
| Clareira tutorial | Andar, pular e primeiro relevo | Piso largo, plataformas baixas e placas curtas |
| Hub central do Bosque | Ponto de retorno e escolha simples | Tres saidas reconheciveis sem excesso de bifurcacoes |
| Caminho inferior das raizes | Rota de combate terrestre | Pisos quebrados, buracos controlados e inimigos basicos |
| Caminho superior das copas | Rota de plataforma e vista do hub | Plataformas justas e retorno por queda controlada |
| Area de combate aberta | Encontro principal antes do chefe | Espaco para recuo, katana e leitura de dano |
| Area de parkour vertical | Subida curta para testar altura | Degraus, plataformas de descanso e `DeathZone` sob quedas |
| Bifurcacao com gate de Dash | Promessa de progressao | Gate memoravel perto de caminho conhecido |
| Atalho bloqueado para o hub | Reduz retorno futuro | Abre ou fica acessivel depois de progressao local |
| Caminho para Lucarelli | Preparacao da arena | Trecho mais controlado e checkpoint proximo |
| Arena de Lucarelli | Pico da demo pre-Dash | Piso seguro, limites claros e chefe testavel |
| Area pos-Dash | Uso imediato e retorno produtivo | Dash atravessa acesso antes bloqueado |
| Fim da demo | Encerramento objetivo | Marco visual, limite fisico e mensagem de fim |

### Papel do hub
O `Hub central do Bosque` e o centro de leitura da demo, nao um conjunto de corredores iguais. Ele deve permitir:
1. chegar da `Clareira tutorial`;
2. descer para o `Caminho inferior das raizes`;
3. subir para o `Caminho superior das copas`;
4. reconhecer um retorno travado ou distante que sera encurtado por atalho;
5. reencontrar a rota pos-Dash sem obrigar o jogador a refazer a entrada inteira.

## Conexoes entre areas
### Grafo de fluxo
```text
[Entrada do Bosque]
        |
        v
[Clareira tutorial]
        |
        v
[Hub central do Bosque]
   |            |
   |            +----------------------+
   v                                   v
[Caminho inferior das raizes]     [Caminho superior das copas]
   |                                   |
   v                                   v
[Area de combate aberta] <---- [Area de parkour vertical]
   |                                   |
   +----------------+------------------+
                    v
          [Bifurcacao com gate de Dash]
             |                    |
             |                    +--> [Area pos-Dash bloqueada antes do chefe]
             v
        [Caminho para Lucarelli]
             |
             v
        [Arena de Lucarelli]
             |
             +--> Dash desbloqueado
                      |
                      v
       [Retorno curto ao gate / atalho para o hub]
                      |
                      v
              [Area pos-Dash]
                      |
                      v
                [Fim da demo]
```

### Conexoes diretas
| Origem | Destino direto | Estado inicial | Motivo |
| --- | --- | --- | --- |
| Entrada do Bosque | Clareira tutorial | Aberto | Entrada sem duvida de direcao |
| Clareira tutorial | Hub central do Bosque | Aberto | Introduz mapa conectado depois do tutorial |
| Hub central do Bosque | Caminho inferior das raizes | Aberto | Rota acessivel sem Dash |
| Hub central do Bosque | Caminho superior das copas | Aberto | Alternativa simples de plataforma |
| Caminho inferior das raizes | Area de combate aberta | Aberto | Consolida combate antes da convergencia |
| Caminho superior das copas | Area de parkour vertical | Aberto | Consolida salto antes da convergencia |
| Area de parkour vertical | Area de combate aberta | Aberto por queda ou plataforma larga | Reune as rotas sem gerar beco longo |
| Area de combate aberta | Bifurcacao com gate de Dash | Aberto | Mostra progressao futura antes do chefe |
| Bifurcacao com gate de Dash | Caminho para Lucarelli | Aberto | Mantem progresso principal claro |
| Bifurcacao com gate de Dash | Area pos-Dash | Bloqueado por Dash | Guarda a rota final da demo |
| Caminho para Lucarelli | Arena de Lucarelli | Aberto | Leva ao chefe |
| Arena de Lucarelli | Retorno curto ao gate | Disponivel apos a luta por layout ou saida segura | Evita backtracking confuso |
| Area pos-Dash | Fim da demo | Aberto depois do gate | Fecha o recorte do Bosque |

### Atalhos
| Atalho | Como aparece | Quando ajuda |
| --- | --- | --- |
| `Shortcut_Combat_To_Hub` | Porta, plataforma de retorno ou barreira simples visivel no hub | Depois de concluir a area de combate aberta |
| `Shortcut_Arena_To_Bifurcation` | Saida curta da arena ate perto do gate | Depois da derrota de Lucarelli |
| `Shortcut_Upper_To_Hub` | Queda segura ou plataforma unidirecional simulada por layout | Se o jogador abandona a rota das copas |

O primeiro blockout pode implementar atalhos apenas com paredes, plataformas e caminhos fisicos. Uma porta destravavel so deve ser criada futuramente se o layout simples nao resolver.

### Onde o Dash muda a rota
- O gate principal fica na `Bifurcacao com gate de Dash`, visivel antes de Lucarelli.
- Antes do Dash, o jogador ve a rota, recebe feedback de bloqueio e segue ao chefe.
- Depois da derrota de Lucarelli, o jogador volta por `Shortcut_Arena_To_Bifurcation` ou por um corredor curto reconhecivel.
- Com Dash liberado, o gate abre e leva a `Area pos-Dash`.
- Dentro da area pos-Dash, uma travessia horizontal curta confirma que a habilidade tem valor alem de abrir o gate.

## Fluxo do jogador
### Primeira passagem
1. Nasce na entrada e aprende leitura basica na clareira.
2. Chega ao hub e entende que existem rotas conectadas.
3. Escolhe a rota inferior mais terrestre ou a rota superior mais vertical.
4. Encontra a convergencia em combate aberto e parkour vertical.
5. Ve o gate de Dash na bifurcacao sem conseguir atravessar.
6. Segue para o caminho de Lucarelli e ativa checkpoint seguro antes da arena.
7. Derrota Lucarelli e recebe o Dash.

### Retorno com habilidade
1. Sai da arena por retorno curto e reconhecivel.
2. Volta ao gate que ja havia visto.
3. Usa a habilidade liberada para entrar na area pos-Dash.
4. Testa Dash em uma travessia simples, sem exigir precisao severa.
5. Alcanca o fim da demo com limite fisico claro.

### Aprendizado por area
| Area | O jogador aprende | Encontros sugeridos |
| --- | --- | --- |
| Entrada do Bosque | Orientacao e limite inicial | Sem inimigo no spawn |
| Clareira tutorial | Andar, pular, aterrissar | Zero ou 1 inimigo distante opcional |
| Hub central | Ler saidas e lembrar caminho | Sem combate apertado |
| Raizes | Lutar no chao e respeitar buracos | 2 inimigos basicos |
| Copas | Subir, pousar e retornar ao hub | 1 inimigo em plataforma larga |
| Combate aberto | Usar espaco e katana | 2 ou 3 inimigos basicos |
| Parkour vertical | Ganhar altura com saltos justos | Zero ou 1 inimigo depois da subida |
| Bifurcacao | Memorizar o gate de Dash | Feedback de `AbilityGate` |
| Caminho de Lucarelli | Preparar ritmo de chefe | 2 inimigos separados |
| Arena | Ler os dois ataques atuais de Lucarelli | Lucarelli |
| Pos-Dash | Reusar area conhecida com habilidade nova | 1 inimigo opcional, nunca em salto critico |
| Fim | Entender encerramento | Sem combate obrigatorio |

## Tempo alvo
### Orcamento de duracao
| Trecho | Duracao alvo |
| --- | --- |
| Entrada e clareira tutorial | `2:00` a `3:00` |
| Primeira leitura do hub | `1:00` a `1:30` |
| Caminho inferior das raizes | `2:30` a `4:00` |
| Caminho superior das copas | `2:30` a `4:00` |
| Combate aberto e parkour vertical | `3:30` a `5:00` |
| Bifurcacao, leitura do gate e caminho ao chefe | `2:30` a `4:00` |
| Arena de Lucarelli | `4:00` a `6:00` |
| Retorno ao gate e area pos-Dash | `2:30` a `4:00` |
| Fim da demo | `0:30` a `1:00` |

### Meta de 20 a 30 minutos
Uma primeira run que percorre somente uma rota do hub deve mirar `20` a `24` minutos. A margem ate `30` minutos vem de:
- explorar a outra rota ou voltar ao hub por curiosidade;
- ler o gate antes de Lucarelli;
- uma queda normal em parkour ou uma tentativa extra na arena;
- testar o Dash na area pos-chefe.

Nao alongue a demo com corredores vazios. Se uma run conhecida cair abaixo de `20` minutos, aumente decisao, encontro ou retorno util; se passar de `30` minutos sem mortes relevantes, reduza distancias entre a convergencia e a arena.

## Metricas tecnicas
As medidas abaixo sao referencias de blockout. Antes de fixar numeros no builder, valide com o pulo e o Dash reais do `PlayerController2D` na cena gerada.

### Salas e conexoes
| Metrica | Alvo inicial |
| --- | --- |
| Largura de sala curta | `14` a `22` unidades |
| Largura media de sala principal | `24` a `38` unidades |
| Largura do hub | `34` a `46` unidades |
| Largura da arena de Lucarelli | `22` a `30` unidades |
| Altura de sala baixa | `7` a `10` unidades |
| Altura media com plataformas | `10` a `15` unidades |
| Altura do parkour vertical | `16` a `24` unidades |
| Corredor de transicao util | `8` a `14` unidades |

### Saltos e plataformas
| Metrica | Alvo sem Dash | Alvo com Dash |
| --- | --- | --- |
| Salto horizontal introdutorio | `2.5` a `3.5` unidades | Nao exigir |
| Salto horizontal normal | `3.5` a `4.5` unidades | Nao exigir |
| Gap de validacao pos-Dash | Nao vencivel de forma consistente sem Dash | `5.5` a `7` unidades |
| Degrau vertical inicial | `1.2` a `2` unidades | Nao exigir |
| Plataforma de descanso | `4` a `7` unidades de largura | Igual |
| Plataforma de pouso depois de risco | Minimo `3.5` unidades de largura | Minimo `4` unidades se houver Dash |

Nao combine gap maximo, subida maxima e inimigo no mesmo salto durante a demo fechada.

### Checkpoints e seguranca
| Metrica | Alvo inicial |
| --- | --- |
| Distancia jogavel entre checkpoints | `45` a `75` unidades de caminho efetivo |
| Checkpoints obrigatorios | `3` |
| Checkpoint recomendado 1 | Saida da clareira ou entrada do hub |
| Checkpoint recomendado 2 | Depois da convergencia de combate e parkour |
| Checkpoint recomendado 3 | Antes da arena de Lucarelli |
| Distancia do respawn ate buraco perigoso | Pelo menos `5` unidades |
| Distancia do respawn ate inimigo | Pelo menos `6` unidades |
| Cobertura de queda | `DeathZone` sob cada buraco e fundo geral do blockout |

### Inimigos e ritmo
| Area | Quantidade aproximada |
| --- | --- |
| Entrada e clareira | `0` a `1` |
| Hub central | `0` a `1` |
| Raizes | `2` a `3` |
| Copas | `1` a `2` |
| Combate aberto | `2` a `3` |
| Parkour vertical | `0` a `1` |
| Caminho de Lucarelli | `2` |
| Pos-Dash | `0` a `1` |

Total recomendado antes do chefe: `7` a `11` inimigos basicos, distribuidos para nao gerar dano inevitavel perto de checkpoints ou plataformas estreitas.

## Regras de design
### Identidade e referencia
- Nao copiar layout, arte, nomes de salas, composicao de encontros ou salas reconheciveis de `Hollow Knight`.
- Usar apenas principios gerais de metroidvania: retorno produtivo, leitura de bloqueio, rotas conectadas e habilidade que recontextualiza caminho.
- Manter o Bosque como lugar proprio de `Tester`, com nevoa, raizes, clareiras e copas apenas como marcadores do tema atual.

### Seguranca do blockout
- O jogador nunca deve cair infinitamente.
- Todo buraco perigoso deve ter `DeathZone` funcional abaixo dele.
- O fundo do mapa deve ter uma `DeathZone` geral ou cobertura equivalente.
- Toda borda externa deve ter limite fisico por parede, piso, teto necessario ou fim fechado.
- Respawns devem cair em piso largo, fora de gate, inimigo, chefe e buraco.

### Leitura de mapa
- O mapa pode ter rotas alternativas simples, mas nao pode virar labirinto.
- Cada sala deve ter uma funcao dominante: orientar, testar movimento, lutar, convergir, preparar chefe ou recompensar Dash.
- Todo bloqueio importante precisa ser visualmente memoravel mesmo com placeholder.
- O gate de Dash deve ser visto antes de ser vencido.
- O retorno depois de Lucarelli deve apontar para algo ja reconhecido, nao para uma saida arbitraria.

### Plataforma e combate
- Colocar plataformas de forma justa para o pulo atual de Rubens.
- Separar salto critico de combate apertado.
- Guardar inimigos em superficies largas quando o foco da area for plataforma.
- Manter a arena de Lucarelli sem buraco fatal na primeira versao da demo expandida.
- Usar pausas curtas entre encontros para HUD, checkpoint e camera respirarem.

### Placeholders
- Dar nomes claros aos grupos e aos objetos de blockout.
- Distinguir visualmente piso, parede, gate, checkpoint, `DeathZone` e fim da demo.
- Usar placas curtas apenas quando o layout e o HUD ainda nao comunicarem o necessario.
- Evitar decorar com volumes que parecam caminho se nao houver colisao ou destino.

## Estrutura sugerida na Hierarchy
```text
Prototype_Bosque_Metroidvania_Demo
|-- Systems
|-- Player
|-- Camera
|-- UI
|-- Level
|   |-- Entrance
|   |-- TutorialClearing
|   |-- CentralHub
|   |-- LowerRoots
|   |-- UpperCanopy
|   |-- OpenCombat
|   |-- VerticalParkour
|   |-- LucarelliPath
|   |-- LucarelliArena
|   |-- PostDash
|-- Enemies
|-- Checkpoints
|-- Gates
|-- Shortcuts
|-- DeathZones
|-- Tutorial
|-- DemoEnd
```

O nome final da cena pode continuar seguindo a convencao adotada pelo projeto quando o builder for atualizado. A estrutura acima descreve organizacao, nao obriga uma nova cena neste prompt.

## Lista de implementacao futura
### Arquivos provavelmente alterados
| Arquivo | Motivo futuro |
| --- | --- |
| `Assets/_Project/Scripts/Editor/PrototypeSceneBuilder.cs` | Gerar blockout maior, grupos, rotas, checkpoints, `DeathZone`, gate, arena e fim |
| `Assets/_Project/Docs/AUTO_SCENE_BUILDER.md` | Explicar nova opcao de cena e fluxo de teste |
| `Assets/_Project/Docs/DEMO_MAP_PLAN.md` | Ajustar o plano linear para refletir a arquitetura conectada, se ainda for a referencia principal |
| `Assets/_Project/Docs/BOSQUE_METROIDVANIA_MAP_ARCHITECTURE.md` | Refinar metricas depois de playtests |

### Scripts que podem ser necessarios depois
| Necessidade | Direcao |
| --- | --- |
| Marcador simples de fim da demo | Reusar UI/log atual se bastar; criar componente pequeno apenas se houver interacao real |
| Atalho controlado | Preferir layout fisico primeiro; criar script apenas se porta travada/destravada ficar necessaria |
| Placa ou trigger tutorial | Usar placeholder atual antes de criar sistema de dialogo |
| Camera por salas | Adiar ate a camera atual falhar no mapa expandido |

`DeathZone`, `Checkpoint`, `AbilityGate`, `AbilityManager`, `LucarelliBoss`, HUD e pausa ja cobrem o fluxo essencial e devem ser reaproveitados antes de criar novos sistemas.

### Trabalho previsto no `PrototypeSceneBuilder`
1. Criar uma opcao de geracao separada para a demo metroidvania ou expandir com nome claro a opcao de demo existente.
2. Montar a entrada, clareira, hub e duas rotas simples com objetos nomeados por regiao.
3. Fazer as rotas inferior e superior convergirem antes da bifurcacao de Dash.
4. Inserir checkpoints seguros nos tres marcos recomendados.
5. Colocar `DeathZone` sob todo buraco e limite geral inferior.
6. Criar paredes de limite externo e fim fechado.
7. Posicionar gate visivel antes do caminho de Lucarelli.
8. Criar retorno curto da arena ao gate ou ao hub.
9. Montar area pos-Dash com travessia simples e marcador de fim.
10. Manter `Prototype_Bosque_Test` e a demo anterior utilizaveis enquanto o novo blockout e validado.

### Partes que continuam placeholders
- Sprites, cor, nevoa, arvores, placas e silhueta final do Bosque.
- Posicoes finais de plataformas ate medir salto real em playtest.
- Distribuicao exata de inimigos e patrulhas.
- Telegraphs visuais, efeitos de hit e presentacao de Lucarelli.
- Comunicacao final de atalho, recompensa e fim da demo.
- Persistencia de checkpoint e Dash entre sessoes.

## Checklist de aceitacao futura do blockout
Quando esta arquitetura virar cena, valide:
1. Rubens chega ao hub em poucos minutos sem se perder.
2. As duas rotas do hub sao distinguiveis e voltam a um fluxo comum.
3. Toda queda perigosa respawna por `DeathZone`.
4. O gate de Dash e visto antes da luta de Lucarelli.
5. Lucarelli libera Dash sem exigir sistema novo.
6. O retorno ao gate depois do chefe e curto e claro.
7. A area pos-Dash exige a habilidade de forma simples.
8. O fim da demo impede avancar para fora do mapa.
9. Uma run nova fica entre `20` e `30` minutos sem inflar deslocamento vazio.

