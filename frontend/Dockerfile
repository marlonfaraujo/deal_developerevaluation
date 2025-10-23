FROM node:22

WORKDIR /workspace/frontend

RUN npm install -g @angular/cli@19

EXPOSE 4200

ENV DEVCONTAINER=true
ENV CHOKIDAR_USEPOLLING=true

USER node

ENTRYPOINT ["tail", "-f", "/dev/null"]


