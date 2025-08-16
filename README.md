# Criptonic

jogo voxel (ainda nao jogavel)


## checklist

# Sistema de Chunks & Geração de Mundo

- [ ] Estrutura dos Chunks  
Cada chunk mantém uma grade de blocos (`Block[,,]`).  
O gerenciador de chunks sempre existe, independente da render distance.  
O parâmetro `chunkDistance` serve apenas para gerenciar meshes visíveis, não a existência do chunk.

- [ ] Geração Procedural  
Estruturas (salas, corredores, cavernas) definem quais chunks precisam existir.  
A geração é instantânea: as dungeons colocam blocos diretamente nos chunks correspondentes.  
Evita chunks vazios inúteis.

- [ ] Modificação de Mundo  
Se a destruição/exposição de um bloco revela o “nada”: criar um chunk vazio ad hoc apenas com os blocos expostos e assumir que o default ao redor é sólido subterrâneo.  
Poupa memória e processamento.

- [ ] Carregamento & Descarregamento  
`Player Enter Event` ativa o chunk atual e dispara o carregamento dos vizinhos.  
Expansão recursiva até o limite de distância (semelhante a A*).  
Descarregamento inteligente: marcar chunks na borda, verificar direção do movimento e descarregar chunks do lado oposto.

- [ ] Renderização & Imersão  
O subterrâneo é naturalmente escuro → chunks descarregados ficam invisíveis.  
Névoa preta cobre ausência de chunks, evitando buracos visuais.  
Permite descarregar chunks agressivamente sem quebrar a imersão.

- [ ] Extras possíveis  
A iluminação do jogador pode ativar temporariamente chunks próximos.  
Implementar sistema de “explorado” vs “não explorado” no minimapa; a dungeon permanece no mapa 2D mesmo se descarregada.
